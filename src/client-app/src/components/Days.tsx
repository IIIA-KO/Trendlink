import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type DaysType = {
  className?: string;
  prop?: string;

  /** Style props */
  propWidth?: CSSProperties["width"];
  propFlex?: CSSProperties["flex"];
  propBorderRadius?: CSSProperties["borderRadius"];
  propBackgroundColor?: CSSProperties["backgroundColor"];
  propOverflow?: CSSProperties["overflow"];
  propColor?: CSSProperties["color"];
  propTextShadow?: CSSProperties["textShadow"];
  propTextDecoration?: CSSProperties["textDecoration"];
};

const Days: FunctionComponent<DaysType> = ({
  className = "",
  propWidth,
  propFlex,
  propBorderRadius,
  propBackgroundColor,
  propOverflow,
  prop,
  propColor,
  propTextShadow,
  propTextDecoration,
}) => {
  const daysStyle: CSSProperties = useMemo(() => {
    return {
      width: propWidth,
      flex: propFlex,
      borderRadius: propBorderRadius,
      backgroundColor: propBackgroundColor,
      overflow: propOverflow,
    };
  }, [
    propWidth,
    propFlex,
    propBorderRadius,
    propBackgroundColor,
    propOverflow,
  ]);

  const div6Style: CSSProperties = useMemo(() => {
    return {
      color: propColor,
      textShadow: propTextShadow,
      textDecoration: propTextDecoration,
    };
  }, [propColor, propTextShadow, propTextDecoration]);

  return (
    <div
      className={`w-6 flex flex-row items-start justify-start pt-0 px-0 pb-[7px] box-border text-center text-sm text-darkslategray-200 font-inter ${className}`}
      style={daysStyle}
    >
      <div className="flex-1 relative uppercase" style={div6Style}>
        {prop}
      </div>
    </div>
  );
};

export default Days;
