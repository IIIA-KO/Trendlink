import { FunctionComponent, memo, useMemo, type CSSProperties } from "react";
import {Component5ExtendType} from "../types/Component5ExtendType";

const Component5: FunctionComponent<Component5ExtendType> = memo(
  ({
    className = "",
    propLeft,
    propBorder,
    propBackgroundColor,
    prop,
    propColor,
    propMinWidth,
    component7Top,
    component7Padding,
    component7Height,
    component7Position,
    divDisplay,
  }) => {
    const component7Style: CSSProperties = useMemo(() => {
      return {
        left: propLeft,
        border: propBorder,
        backgroundColor: propBackgroundColor,
        top: component7Top,
        padding: component7Padding,
        height: component7Height,
        position: component7Position,
      };
    }, [
      propLeft,
      propBorder,
      propBackgroundColor,
      component7Top,
      component7Padding,
      component7Height,
      component7Position,
    ]);

    const divStyle: CSSProperties = useMemo(() => {
      return {
        color: propColor,
        minWidth: propMinWidth,
        display: divDisplay,
      };
    }, [propColor, propMinWidth, divDisplay]);

    return (
      <div
        className={`absolute top-[39.25rem] left-[58.625rem] rounded-[5px] border-[#c0bebe] border-[1px] border-solid overflow-hidden flex flex-row items-start justify-start py-[0.562rem] pl-[3.375rem] pr-[3.312rem] z-[1] text-left text-[0.875rem] text-[#3c3c3c] font-[Inter] ${className}`}
        style={component7Style}
      >
        <div
          className="relative inline-block min-w-[4.563rem]"
          style={divStyle}
        >
          {prop}
        </div>
      </div>
    );
  }
);

export default Component5;
