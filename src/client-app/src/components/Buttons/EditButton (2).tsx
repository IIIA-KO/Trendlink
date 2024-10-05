import { FunctionComponent, memo, useMemo, type CSSProperties } from "react";
import { EditButtonExtendType } from "../../types/EditButtonExtendType";

const EditButton: FunctionComponent<EditButtonExtendType> = memo(
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
