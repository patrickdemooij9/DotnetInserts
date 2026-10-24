export interface InsertRenderResult {
    insertName: string;
    stlFilePath: string;
    isExtra: boolean;
    /** Index of the visible part this STL is, or (for an extra) was generated from. */
    partIndex: number;
}