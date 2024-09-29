import type {CSSProperties} from "react";

export type Component1Type = {
    className?: string;

    /** Style props */
    propFlex?: CSSProperties["flex"];
    propPosition?: CSSProperties["position"];
    propTop?: CSSProperties["top"];
    propLeft?: CSSProperties["left"];
    propWidth?: CSSProperties["width"];
};