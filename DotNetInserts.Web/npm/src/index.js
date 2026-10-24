import * as THREE from 'three'
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls'
import { STLLoader } from 'three/examples/jsm/loaders/STLLoader'

window.loadStl = function (elementId) {
    var elem = document.getElementById(elementId);

    var camera = new THREE.PerspectiveCamera(70, elem.clientWidth / elem.clientHeight, 1, 1000);

    var renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
    renderer.setSize(elem.clientWidth, elem.clientHeight);
    renderer.shadowMap.enabled = true;

    elem.appendChild(renderer.domElement);

    window.addEventListener("resize", function () {
        renderer.setSize(elem.clientWidth, elem.clientHeight);
        camera.aspect = elem.clientWidth / elem.clientHeight;
        camera.updateProjectionMatrix();
    }, false);

    var controls = new OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.rotateSpeed = 1;
    controls.dampingFactor = 0.1;
    controls.enableZoom = true;
    //controls.autoRotate = true;
    //controls.autoRotateSpeed = .75;

    var scene = new THREE.Scene();
    scene.add(new THREE.DirectionalLight(0xffffff, 1.5));

    var gridHelper = new THREE.GridHelper(1000, 10);
    scene.add(gridHelper);

    var test = null;

    (new STLLoader()).load("/assets/test2.stl", function (geometry) {
        var material = new THREE.MeshPhongMaterial({
            color: 0xff5533,
            specular: 100,
            shininess: 100
        });

        test = geometry;
        //geometry.rotateX(-90);
        //geometry.rotateZ(90);

        var mesh = new THREE.Mesh(geometry, material);
        scene.add(mesh);

        var middle = new THREE.Vector3();
        //geometry.computeBoundingBox();
        //geometry.boundingBox.getCenter(middle);
        //mesh.geometry.applyMatrix4(new THREE.Matrix4().makeTranslation(-middle.x, -middle.y, -middle.z));

        //var largestDimension = Math.max(geometry.boundingBox.max.x, geometry.boundingBox.max.y, geometry.boundingBox.max.z);
        camera.position.z = 20;
    });

    function animate() {
        requestAnimationFrame(animate);

        test?.rotateX(0.01);
        console.log(test?.rotation);

        controls.update();
        renderer.render(scene, camera);
    }

    animate();
}