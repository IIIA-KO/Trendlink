import type {CSSProperties} from "react";

export type ProfileStatisticsContainerType = {
    className?: string;
    bG?: string;

    /** Style props */
    propFlex?: CSSProperties["flex"];
    propAlignSelf?: CSSProperties["alignSelf"];
    propHeight?: CSSProperties["height"];
    propLeft?: CSSProperties["left"];
};