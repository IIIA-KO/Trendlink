import { FunctionComponent, memo, useMemo, type CSSProperties } from "react";

export type EditButtonType = {
  className?: string;
  prop?: string;

  /** Style props */
  editButtonBorderRadius?: CSSProperties["borderRadius"];
  editButtonBackgroundColor?: CSSProperties["backgroundColor"];
  editButtonOverflow?: CSSProperties["overflow"];
  editButtonDisplay?: CSSProperties["display"];
  editButtonFlexDirection?: CSSProperties["flexDirection"];
  editButtonPadding?: CSSProperties["padding"];
  editButtonPosition?: CSSProperties["position"];
  editButtonTop?: CSSProperties["top"];
  editButtonLeft?: CSSProperties["left"];
  divDisplay?: CSSProperties["display"];
  divMinWidth?: CSSProperties["minWidth"];
};

const EditButton: FunctionComponent<EditButtonType> = memo(
  ({
    className = "",
    editButtonBorderRadius,
    editButtonBackgroundColor,
    editButtonOverflow,
    editButtonDisplay,
    editButtonFlexDirection,
    editButtonPadding,
    editButtonPosition,
    editButtonTop,
    editButtonLeft,
    prop,
    divDisplay,
    divMinWidth,
  }) => {
    const editButtonStyle: CSSProperties = useMemo(() => {
      return {
        borderRadius: editButtonBorderRadius,
        backgroundColor: editButtonBackgroundColor,
        overflow: editButtonOverflow,
        display: editButtonDisplay,
        flexDirection: editButtonFlexDirection,
        padding: editButtonPadding,
        position: editButtonPosition,
        top: editButtonTop,
        left: editButtonLeft,
      };
    }, [
      editButtonBorderRadius,
      editButtonBackgroundColor,
      editButtonOverflow,
      editButtonDisplay,
      editButtonFlexDirection,
      editButtonPadding,
      editButtonPosition,
      editButtonTop,
      editButtonLeft,
    ]);

    const div3Style: CSSProperties = useMemo(() => {
      return {
        display: divDisplay,
        minWidth: divMinWidth,
      };
    }, [divDisplay, divMinWidth]);

    return (
      <div
        className={`rounded-[40px] bg-[#009ea0] overflow-hidden flex flex-row items-start justify-start py-[0.625rem] px-[3.5rem] whitespace-nowrap text-left text-[0.875rem] text-[#fff] font-[Inter] ${className}`}
        style={editButtonStyle}
      >
        <div className="relative" style={div3Style}>
          {prop}
        </div>
      </div>
    );
  }
);

export default EditButton;
