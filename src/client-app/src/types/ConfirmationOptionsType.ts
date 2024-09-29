import type {CSSProperties} from "react";
import {ClassNameType} from "./ClassNameType";

export type ConfirmationOptionsType = ClassNameType & {
    prop?: string;

    /** Style props */
    propMinWidth?: CSSProperties["minWidth"];
};