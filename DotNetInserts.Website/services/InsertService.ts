import type { InsertRenderResult } from "~/models/InsertRenderResult";
import type { DotNetInsertsRoot } from "./InsertModels";
import type { UploadedImage } from "~/models/UploadedImage";

/** A big insert is several OpenSCAD runs; past this something is stuck, not slow. */
const RenderTimeoutMs = 5 * 60 * 1000;

let apiBase = "https://localhost:7016";

export function setApiBase(base: string) {
  apiBase = base.replace(/\/+$/, "");
}

export function apiUrl(path: string) {
  return `${apiBase}/${path.replace(/^\/+/, "")}`;
}

/** Turns whatever fetch threw or the API answered into one sentence for the user. */
async function request(path: string, init: RequestInit): Promise<Response> {
  let response: Response;
  try {
    response = await fetch(apiUrl(path), init);
  } catch (error) {
    if (error instanceof DOMException && error.name === "TimeoutError") {
      throw new Error("The render server took too long to answer. Try again, or simplify the insert.");
    }
    throw new Error("Could not reach the render server. Check your connection and try again.");
  }

  if (!response.ok) {
    const text = (await response.text().catch(() => "")).trim();
    // ASP.NET answers validation failures with a problem+json body; its title is the readable bit.
    let message = text;
    try {
      const problem = JSON.parse(text);
      message = problem.detail ?? problem.title ?? text;
    } catch {
      // Plain-text body; use as is.
    }
    throw new Error(message && message.length < 500 ? message : `The server answered with an error (${response.status}).`);
  }

  return response;
}

export async function SendToServer(root: DotNetInsertsRoot): Promise<InsertRenderResult[]> {
  const result = await request("insert", {
    method: "POST",
    body: JSON.stringify(root),
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    signal: AbortSignal.timeout(RenderTimeoutMs),
  });

  return (await result.json()) as InsertRenderResult[];
}

/**
 * Copies an SVG onto the machine that runs the renders and returns where it landed.
 *
 * The browser only ever gets a File object, never a path OpenSCAD could import, so the
 * file has to make this trip before an `svg` component can refer to it.
 */
export async function UploadImage(file: File): Promise<UploadedImage> {
  const body = new FormData();
  body.append("file", file);

  const result = await request("insert/image", {
    method: "POST",
    body,
  });

  return (await result.json()) as UploadedImage;
}
