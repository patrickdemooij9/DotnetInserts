import type { Component } from "vue";

export default interface BuilderOverlayModel {
    component: Component;
    props?: Record<string, any>;
}