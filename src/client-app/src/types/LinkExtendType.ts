import type {CSSProperties} from "react";
import {LinkBaseType} from "./LinkBaseType";

export type LinkExtendType = LinkBaseType &  {
    divDisplay?: CSSProperties["display"];
};