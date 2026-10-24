<script lang="ts" setup>
import { OrbitControls, STLLoader } from "three/examples/jsm/Addons.js";
import {
  AmbientLight,
  Box3,
  BoxGeometry,
  DirectionalLight,
  EdgesGeometry,
  GridHelper,
  Group,
  LineBasicMaterial,
  LineSegments,
  Mesh,
  MeshPhongMaterial,
  PerspectiveCamera,
  Scene,
  Vector3,
  WebGLRenderer,
} from "three";

const props = defineProps<{
  filePaths: string[];
  isLoading: boolean;
  modelSize: { x: number; y: number; z: number };
}>();

var scene: Scene;
var modelGroup: Group;
var sizeBoxLines: LineSegments | null = null;
var frameModel: ((box: Box3) => void) | null = null;

const isLoadingStl = ref(false);
const loadError = ref<string | null>(null);
const isLoading = computed(() => props.isLoading || isLoadingStl.value);

const container = ref<HTMLElement | null>(null);
let disposeViewer: (() => void) | null = null;
// Bumped per load so a slow STL from a previous render cannot land in the current one.
let loadGeneration = 0;

watch(
  () => props.filePaths,
  () => {
    if (props.filePaths.length > 0) {
      loadStls();
    }
  }
);

onBeforeUnmount(() => disposeViewer?.());

watch(() => props.modelSize, updateSizeBox, { deep: true });

onMounted(() => {
  const elem = container.value!;

  var camera = new PerspectiveCamera(
    70,
    elem.clientWidth / elem.clientHeight,
    1,
    1000
  );

  var renderer = new WebGLRenderer({ antialias: true, alpha: true });
  renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
  renderer.setSize(elem.clientWidth, elem.clientHeight);
  renderer.shadowMap.enabled = true;

  renderer.domElement.style.display = "block";
  elem.prepend(renderer.domElement);

  // Follows the panel rather than the window, so a layout change (mobile stacking, the
  // sidebar growing) resizes the canvas too.
  const resizeObserver = new ResizeObserver(() => {
    if (elem.clientWidth === 0 || elem.clientHeight === 0) return;
    renderer.setSize(elem.clientWidth, elem.clientHeight);
    camera.aspect = elem.clientWidth / elem.clientHeight;
    camera.updateProjectionMatrix();
  });
  resizeObserver.observe(elem);

  var controls = new OrbitControls(camera, renderer.domElement);
  controls.enableDamping = true;
  controls.rotateSpeed = 1;
  controls.dampingFactor = 0.1;
  controls.enableZoom = true;

  // Points the camera at a freshly loaded model from the same angle the viewer opens at,
  // far enough back that all of it is in view.
  frameModel = (box: Box3) => {
    const center = box.getCenter(new Vector3());
    const radius = Math.max(box.getSize(new Vector3()).length() / 2, 10);
    // Whichever of the vertical and horizontal field of view is narrower decides the fit.
    const halfVertical = (camera.fov * Math.PI) / 360;
    const halfHorizontal = Math.atan(Math.tan(halfVertical) * camera.aspect);
    const distance = (radius / Math.sin(Math.min(halfVertical, halfHorizontal))) * 1.1;
    const direction = new Vector3(0, 300, 200).normalize();
    controls.target.copy(center);
    camera.position.copy(center).addScaledVector(direction, distance);
    camera.far = Math.max(1000, distance * 4);
    camera.updateProjectionMatrix();
  };

  scene = new Scene();
  scene.add(new AmbientLight(0xffffff, 0.8));
  const light = new DirectionalLight(0xffffff, 1.5);
  light.position.set(1, 2, 1.5);
  scene.add(light);

  modelGroup = new Group();
  scene.add(modelGroup);

  var gridHelper = new GridHelper(1000, 50);
  scene.add(gridHelper);

  camera.position.z = 200;
  camera.position.y = 300;

  updateSizeBox();

  let frame = 0;
  function animate() {
    frame = requestAnimationFrame(animate);
    controls.update();
    renderer.render(scene, camera);
  }

  animate();

  disposeViewer = () => {
    cancelAnimationFrame(frame);
    resizeObserver.disconnect();
    controls.dispose();
    renderer.dispose();
    renderer.domElement.remove();
  };
});

function updateSizeBox() {
  if (!modelGroup) return;
  if (sizeBoxLines) modelGroup.remove(sizeBoxLines);

  const { x, y, z } = props.modelSize;
  if (x <= 0 || y <= 0 || z <= 0) return;

  // After rotateX(-PI/2): OpenSCAD(x,y,z) → Three.js(x, z, -y)
  // BoxGeometry is centered at origin, so offset to (x/2, z/2, -y/2)
  const edges = new EdgesGeometry(new BoxGeometry(x, z, y));
  sizeBoxLines = new LineSegments(edges, new LineBasicMaterial({ color: 0x4488ff }));
  sizeBoxLines.position.set(x / 2, z / 2, -y / 2);
  modelGroup.add(sizeBoxLines);
}

function loadStls() {
  const generation = ++loadGeneration;
  isLoadingStl.value = true;
  loadError.value = null;
  modelGroup.clear();
  modelGroup.position.set(0, 0, 0);
  sizeBoxLines = null;

  const paths = [...props.filePaths];
  if (paths.length === 0) {
    isLoadingStl.value = false;
    updateSizeBox();
    return;
  }

  let loadedCount = 0;
  const material = new MeshPhongMaterial({
    color: 0xff5533,
    specular: 100,
    shininess: 100,
  });

  let failedCount = 0;
  const finish = () => {
    if (loadedCount + failedCount < paths.length) return;
    isLoadingStl.value = false;
    if (failedCount > 0) {
      loadError.value =
        failedCount === paths.length
          ? "The rendered model could not be loaded. Try rendering again."
          : `${failedCount} of ${paths.length} parts could not be loaded. Try rendering again.`;
    }
  };

  for (const filePath of paths) {
    new STLLoader().load(filePath, (geometry) => {
      if (generation !== loadGeneration) return;
      geometry.rotateX(-Math.PI / 2);
      modelGroup.add(new Mesh(geometry, material));

      loadedCount++;
      if (loadedCount + failedCount === paths.length) {
        // Center based on model meshes only, before adding the size box
        const box = new Box3().setFromObject(modelGroup);
        const center = new Vector3();
        box.getCenter(center);
        modelGroup.position.set(-center.x, 0, -center.z);
        frameModel?.(new Box3().setFromObject(modelGroup));

        updateSizeBox();
      }
      finish();
    }, undefined, () => {
      if (generation !== loadGeneration) return;
      failedCount++;
      finish();
    });
  }
}
</script>

<template>
  <div ref="container" class="h-full w-full bg-gray-100 relative">
    <div v-if="isLoading" class="absolute inset-0 flex items-center justify-center pointer-events-none">
      <span class="flex items-center gap-3 text-gray-700 bg-white rounded-xl border shadow-sm px-4 py-3">
        <span class="h-4 w-4 rounded-full border-2 border-gray-300 border-t-blue-500 animate-spin"></span>
        Rendering… this can take a minute for large inserts
      </span>
    </div>
    <div v-else-if="loadError" class="absolute inset-0 flex items-center justify-center pointer-events-none">
      <span class="text-red-700 bg-white rounded-xl border border-red-200 shadow-sm px-4 py-3">{{ loadError }}</span>
    </div>
    <div v-else-if="!props.filePaths.length" class="absolute inset-0 flex items-center justify-center pointer-events-none p-4">
      <span class="text-gray-600 bg-white rounded-xl border shadow-sm px-4 py-3 text-center max-w-sm">
        Nothing rendered yet. Add a part on the left, then press <strong>Render</strong> to see it here.
      </span>
    </div>
    <div class="absolute bottom-2 right-3 text-xs text-gray-500 pointer-events-none select-none hidden sm:block">
      Drag to rotate · scroll to zoom · right-drag to pan
    </div>
  </div>
</template>
