import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type ButtonType = {
  className?: string;

  /** Style props */
  propRight?: CSSProperties["right"];
  propLeft?: CSSProperties["left"];
  propBackgroundColor?: CSSProperties["backgroundColor"];
};

const Button: FunctionComponent<ButtonType> = ({
  className = "",
  propRight,
  propLeft,
  propBackgroundColor,
}) => {
  const errorButtonShapeStyle: CSSProperties = useMemo(() => {
    return {
      right: propRight,
      left: propLeft,
      backgroundColor: propBackgroundColor,
    };
  }, [propRight, propLeft, propBackgroundColor]);

  return (
    <div className={`h-6 w-[47px] relative z-[1] ${className}`}>
      <div className="absolute h-full w-full top-[0%] right-[0%] bottom-[0%] left-[0%] rounded-mini bg-whitesmoke-200" />
      <div
        className="absolute h-full w-[48.94%] top-[0%] right-[0%] bottom-[0%] left-[51.06%] rounded-mini bg-main-green z-[1]"
        style={errorButtonShapeStyle}
      />
    </div>
  );
};

export default Button;
