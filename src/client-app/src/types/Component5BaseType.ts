import type {CSSProperties} from "react";

export type Component5BaseType = {
    className?: string;
    prop?: string;

    /** Style props */
    propLeft?: CSSProperties["left"];
    propBorder?: CSSProperties["border"];
    propBackgroundColor?: CSSProperties["backgroundColor"];
    propColor?: CSSProperties["color"];
    propMinWidth?: CSSProperties["minWidth"];
};