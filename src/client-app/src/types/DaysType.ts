import type {CSSProperties} from "react";

export type DaysType = {
    className?: string;
    prop?: string;

    /** Style props */
    propWidth?: CSSProperties["width"];
    propFlex?: CSSProperties["flex"];
    propBorderRadius?: CSSProperties["borderRadius"];
    propBackgroundColor?: CSSProperties["backgroundColor"];
    propOverflow?: CSSProperties["overflow"];
    propColor?: CSSProperties["color"];
    propTextShadow?: CSSProperties["textShadow"];
    propTextDecoration?: CSSProperties["textDecoration"];
};