import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type Component5Type = {
  className?: string;
  prop?: string;

  /** Style props */
  propLeft?: CSSProperties["left"];
  propBorder?: CSSProperties["border"];
  propBackgroundColor?: CSSProperties["backgroundColor"];
  propColor?: CSSProperties["color"];
  propMinWidth?: CSSProperties["minWidth"];
};

const Component5: FunctionComponent<Component5Type> = ({
  className = "",
  propLeft,
  propBorder,
  propBackgroundColor,
  prop,
  propColor,
  propMinWidth,
}) => {
  const component7Style: CSSProperties = useMemo(() => {
    return {
      left: propLeft,
      border: propBorder,
      backgroundColor: propBackgroundColor,
    };
  }, [propLeft, propBorder, propBackgroundColor]);

  const divStyle: CSSProperties = useMemo(() => {
    return {
      color: propColor,
      minWidth: propMinWidth,
    };
  }, [propColor, propMinWidth]);

  return (
    <div
      className={`absolute top-[628px] left-[938px] rounded-8xs border-transparent border-[1px] border-solid overflow-hidden flex flex-row items-start justify-start py-[9px] pl-[54px] pr-[53px] z-[1] text-left text-sm text-main-black font-inter ${className}`}
      style={component7Style}
    >
      <div className="relative inline-block min-w-[73px]" style={divStyle}>
        {prop}
      </div>
    </div>
  );
};

export default Component5;
