/**
 * Where the API put an uploaded SVG. `imagePath` is what the `svg` component stores and
 * OpenSCAD imports; `previewUrl` only exists so the builder can show which file it is.
 */
export interface UploadedImage {
    imagePath: string;
    previewUrl: string;
}
