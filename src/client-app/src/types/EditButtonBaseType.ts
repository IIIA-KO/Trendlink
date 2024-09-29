import type {CSSProperties} from "react";
import {ClassNameType} from "./ClassNameType";

export type EditButtonBaseType = ClassNameType & {

    /** Style props */
    editButtonBorderRadius?: CSSProperties["borderRadius"];
    editButtonBackgroundColor?: CSSProperties["backgroundColor"];
    editButtonOverflow?: CSSProperties["overflow"];
    editButtonDisplay?: CSSProperties["display"];
    editButtonFlexDirection?: CSSProperties["flexDirection"];
    editButtonPadding?: CSSProperties["padding"];
};