import { ref } from "vue";
import type BuilderOverlayModel from "./BuilderOverlayModel";
import BoxComponentBuilder from "./partComponents/BoxComponentBuilder.vue";
import CircleComponentBuilder from "./partComponents/CircleComponentBuilder.vue";
import CardTrayComponentBuilder from "./partComponents/CardTrayComponentBuilder.vue";
import type { PartComponent } from "~/services/InsertModels";
import RepeatableComponentBuilder from "./partComponents/RepeatableComponentBuilder.vue";
import SvgComponentBuilder from "./partComponents/SvgComponentBuilder.vue";
export class BuilderOverlayHelper {
    public static Overlays = ref<BuilderOverlayModel[]>([]);
}

const components: { [key: string]: Component } = {
  "box": BoxComponentBuilder,
  "circle": CircleComponentBuilder,
  "cardTray": CardTrayComponentBuilder,
  "repeatable": RepeatableComponentBuilder,
  "svg": SvgComponentBuilder
};

export function OpenComponentPanel(partComponent: PartComponent) {
  BuilderOverlayHelper.Overlays.value.push({
    component: components[partComponent!.$type],
    props: {
      partComponent: partComponent,
    },
  });
}