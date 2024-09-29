import { FunctionComponent, useMemo, type CSSProperties } from "react";
import {EditButtonBaseType} from "../../types/EditButtonBaseType";

const EditButton: FunctionComponent<EditButtonBaseType> = ({
  className = "",
  editButtonBorderRadius,
  editButtonBackgroundColor,
  editButtonOverflow,
  editButtonDisplay,
  editButtonFlexDirection,
  editButtonPadding,
}) => {
  const editButtonStyle: CSSProperties = useMemo(() => {
    return {
      borderRadius: editButtonBorderRadius,
      backgroundColor: editButtonBackgroundColor,
      overflow: editButtonOverflow,
      display: editButtonDisplay,
      flexDirection: editButtonFlexDirection,
      padding: editButtonPadding,
    };
  }, [
    editButtonBorderRadius,
    editButtonBackgroundColor,
    editButtonOverflow,
    editButtonDisplay,
    editButtonFlexDirection,
    editButtonPadding,
  ]);

  return (
    <div
      className={`rounded-21xl bg-main-green overflow-hidden  flex flex-row items-start justify-start py-2.5 px-14 whitespace-nowrap text-left text-sm text-white font-inter ${className}`}
      style={editButtonStyle}
    >
      <div className="relative">Редагувати профіль</div>
    </div>
  );
};

export default EditButton;
