import type {CSSProperties} from "react";

export type ProfileHeaderType = {
    className?: string;
    profilePicture?: string;
    group95?: string;
    editButtonBorderRadius?: string;
    editButtonBackgroundColor?: string;
    editButtonOverflow?: string;
    editButtonDisplay?: string;
    editButtonFlexDirection?: string;
    editButtonPadding?: string;

    /** Style props */
    groupDivLeft?: CSSProperties["left"];
    groupDivTop?: CSSProperties["top"];
    groupDivRight?: CSSProperties["right"];
    divTextDecoration?: CSSProperties["textDecoration"];
};