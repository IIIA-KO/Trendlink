import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type Component6Type = {
  className?: string;
  prop?: string;
  prop1?: string;

  /** Style props */
  propAlignSelf?: CSSProperties["alignSelf"];
  propOverflow?: CSSProperties["overflow"];
  propFlex?: CSSProperties["flex"];
  propOverflow1?: CSSProperties["overflow"];
  propHeight?: CSSProperties["height"];
  propWidth?: CSSProperties["width"];
};

const Component6: FunctionComponent<Component6Type> = ({
  className = "",
  propAlignSelf,
  propOverflow,
  propFlex,
  prop,
  propOverflow1,
  propHeight,
  prop1,
  propWidth,
}) => {
  const component2Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: propAlignSelf,
      overflow: propOverflow,
      flex: propFlex,
    };
  }, [propAlignSelf, propOverflow, propFlex]);

  const iconStyle: CSSProperties = useMemo(() => {
    return {
      overflow: propOverflow1,
      height: propHeight,
    };
  }, [propOverflow1, propHeight]);

  const div2Style: CSSProperties = useMemo(() => {
    return {
      width: propWidth,
    };
  }, [propWidth]);

  return (
    <div
      className={`self-stretch overflow-hidden flex flex-row items-start justify-start pt-[10.3px] px-[42px] pb-[10.4px] gap-[25px] text-left text-base text-white font-inter ${className}`}
      style={component2Style}
    >
      <img
        className="h-[34.3px] w-[35px] relative overflow-hidden shrink-0"
        loading="lazy"
        alt=""
        src={prop}
        style={iconStyle}
      />
      <div className="w-[171px] relative uppercase hidden" style={div2Style}>
        {prop1}
      </div>
    </div>
  );
};

export default Component6;
