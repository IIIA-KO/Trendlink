import type {CSSProperties} from "react";
import {EditButtonBaseType} from "./EditButtonBaseType";

export type EditButtonExtendType = EditButtonBaseType & {
    prop?: string;

    /** Style props */
    editButtonPosition?: CSSProperties["position"];
    editButtonTop?: CSSProperties["top"];
    editButtonLeft?: CSSProperties["left"];
    divDisplay?: CSSProperties["display"];
    divMinWidth?: CSSProperties["minWidth"];
};