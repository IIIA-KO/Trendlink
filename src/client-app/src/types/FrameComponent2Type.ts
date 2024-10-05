import type {CSSProperties} from "react";
import {ClassNameType} from "./ClassNameType";

export type FrameComponent2Type = ClassNameType &{
    bG?: string;

    /** Style props */
    propTop?: CSSProperties["top"];
    propHeight?: CSSProperties["height"];
    propTop1?: CSSProperties["top"];
    propTop2?: CSSProperties["top"];
    propWidth?: CSSProperties["width"];
    propDisplay?: CSSProperties["display"];
    propMinWidth?: CSSProperties["minWidth"];
    propDisplay1?: CSSProperties["display"];
    propMinWidth1?: CSSProperties["minWidth"];
    propMinWidth2?: CSSProperties["minWidth"];
};