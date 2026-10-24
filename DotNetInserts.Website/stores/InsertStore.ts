import { defineStore } from "pinia";
import { migrateInsert } from "~/services/CutoutMigration";
import type { DotNetInsertsRoot } from "~/services/InsertModels";

export const useInsertStore = defineStore('insert', {
  state: () => ({
    model: {
      name: '',
      size: {
        x: 0,
        y: 0,
        z: 0
      },
      parts: []
    } as DotNetInsertsRoot
  }),
  getters: {
    parts: (state) => state.model.parts || [],
  },
  actions: {
    setModel: function (model: DotNetInsertsRoot) {
      // Single entry point for imported JSON, so this is where an old-schema file
      // gets upgraded to `cutouts`.
      migrateInsert(model);

      this.model.name = model.name;
      this.model.parts = model.parts || [];
      this.model.size = model.size;
    },
    reset: function () {
      this.model.name = '';
      this.model.parts = [];
      this.model.size = { x: 0, y: 0, z: 0 };
    }
  }
});