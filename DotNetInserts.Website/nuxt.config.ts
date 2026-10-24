// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2025-05-15',
  devtools: { enabled: true },
  modules: ['@nuxtjs/tailwindcss','@pinia/nuxt'],

  runtimeConfig: {
    public: {
      // Where DotNetInserts.Api runs. Override at deploy time with NUXT_PUBLIC_API_BASE.
      apiBase: 'https://localhost:7016',
    },
  },

  routeRules: {
    '/': { redirect: '/InsertBuilder' },
  },

  app: {
    head: {
      htmlAttrs: { lang: 'en' },
      // The UI is only designed in light colours; without this a dark-mode OS paints the
      // page background dark under grey text.
      bodyAttrs: { class: 'bg-white text-gray-900' },
      title: 'DotNetInserts – board game insert builder',
      meta: [
        { name: 'description', content: 'Design parametric 3D-printable board game inserts in your browser and download the STLs.' },
        { name: 'theme-color', content: '#ffffff' },
        { name: 'color-scheme', content: 'light' },
      ],
    },
  },
})
