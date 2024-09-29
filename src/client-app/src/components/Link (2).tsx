import { FunctionComponent, memo, useMemo, type CSSProperties } from "react";
import {LinkExtendType} from "../types/LinkExtendType";

const Link: FunctionComponent<LinkExtendType> = memo(
  ({
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
    divDisplay,
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
        display: divDisplay,
      };
    }, [
      propPosition1,
      propMargin,
      propTop,
      propLeft,
      propMinWidth,
      divDisplay,
    ]);

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
        className={`self-stretch h-[1.313rem] flex flex-col items-start justify-start relative gap-[0.125rem] z-[1] text-left text-[1rem] text-[#3c3c3c] font-[Inter] ${className}`}
        style={linkStyle}
      >
        <div
          className="absolute !m-[0] top-[0rem] left-[0rem] inline-block min-w-[3.313rem]"
          style={div1Style}
        >
          {prop}
        </div>
        <img
          className="w-[0rem] h-[0.063rem] absolute !m-[0] top-[1.313rem] left-[0rem]"
          loading="lazy"
          alt=""
          src={vector1}
          style={vectorIconStyle}
        />
      </div>
    );
  }
);

export default Link;
