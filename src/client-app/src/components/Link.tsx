import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type LinkType = {
  className?: string;
  prop?: string;
  vector1?: string;

  /** Style props */
  propAlignSelf?: CSSProperties["alignSelf"];
  propHeight?: CSSProperties["height"];
  propPosition?: CSSProperties["position"];
  propPosition1?: CSSProperties["position"];
  propMargin?: CSSProperties["margin"];
  propTop?: CSSProperties["top"];
  propLeft?: CSSProperties["left"];
  propMinWidth?: CSSProperties["minWidth"];
  propWidth?: CSSProperties["width"];
  propPosition2?: CSSProperties["position"];
  propMargin1?: CSSProperties["margin"];
  propTop1?: CSSProperties["top"];
  propLeft1?: CSSProperties["left"];
};

const Link: FunctionComponent<LinkType> = ({
  className = "",
  propAlignSelf,
  propHeight,
  propPosition,
  prop,
  propPosition1,
  propMargin,
  propTop,
  propLeft,
  propMinWidth,
  vector1,
  propWidth,
  propPosition2,
  propMargin1,
  propTop1,
  propLeft1,
}) => {
  const linkStyle: CSSProperties = useMemo(() => {
    return {
      alignSelf: propAlignSelf,
      height: propHeight,
      position: propPosition,
    };
  }, [propAlignSelf, propHeight, propPosition]);

  const div1Style: CSSProperties = useMemo(() => {
    return {
      position: propPosition1,
      margin: propMargin,
      top: propTop,
      left: propLeft,
      minWidth: propMinWidth,
    };
  }, [propPosition1, propMargin, propTop, propLeft, propMinWidth]);

  const vectorIconStyle: CSSProperties = useMemo(() => {
    return {
      width: propWidth,
      position: propPosition2,
      margin: propMargin1,
      top: propTop1,
      left: propLeft1,
    };
  }, [propWidth, propPosition2, propMargin1, propTop1, propLeft1]);

  return (
    <div
      className={`self-stretch h-[21px] flex flex-col items-start justify-start relative gap-0.5 z-[1] text-left text-base text-main-black font-inter ${className}`}
      style={linkStyle}
    >
      <div
        className="absolute !m-[0] top-[0px] left-[0px] inline-block min-w-[53px]"
        style={div1Style}
      >
        {prop}
      </div>
      <img
        className="w-0 h-px absolute !m-[0] top-[21px] left-[0px]"
        loading="lazy"
        alt=""
        src={vector1}
        style={vectorIconStyle}
      />
    </div>
  );
};

export default Link;
