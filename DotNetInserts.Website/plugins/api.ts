import { setApiBase } from "~/services/InsertService";

// The services are plain functions called from event handlers, where the Nuxt context is
// not reliably available, so the base URL is handed to them once at startup instead.
export default defineNuxtPlugin(() => {
  setApiBase(useRuntimeConfig().public.apiBase as string);
});
