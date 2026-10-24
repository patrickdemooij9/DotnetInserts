# DotNetInserts website

The browser builder for DotNetInserts: edit an insert, send it to `DotNetInserts.Api` to render with OpenSCAD, preview the STLs and download them.

## Development

```bash
npm install
npm run dev
```

The site runs on http://localhost:3000 and expects the API at `https://localhost:7016` (the API's `https` launch profile).

## Configuration

| Setting | Where | Purpose |
| --- | --- | --- |
| `NUXT_PUBLIC_API_BASE` | website env | Public URL of the API, e.g. `https://api.example.com`. |
| `PublicBaseUrl` | API config / `PublicBaseUrl` env | URL the API puts in STL and image links. Needed behind a reverse proxy; otherwise taken from the request. |
| `Cors__AllowedOrigins__0` (etc.) | API config | Origin(s) of the website. When none are set, any origin is allowed, which is only meant for local development. |

## Production

```bash
npm run build
NUXT_PUBLIC_API_BASE=https://api.example.com node .output/server/index.mjs
```

Users' work is autosaved in their browser's localStorage; "Download JSON" is the only way to move it to another browser.
