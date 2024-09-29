import { FunctionComponent, memo } from "react";
import Component5 from "./Component5 (2)";
import {ProfileNavigationType} from "../types/ProfileNavigationType";

const ProfileNavigation: FunctionComponent<ProfileNavigationType> = memo(
  ({
    className = "",
    prop,
    prop1,
    prop2,
    prop3,
    prop4,
    propLeft,
    propLeft1,
    propLeft2,
    propLeft3,
    propLeft4,
    propBorder,
    propBorder1,
    propBorder2,
    propBorder3,
    propBorder4,
    propBackgroundColor,
    propBackgroundColor1,
    propBackgroundColor2,
    propBackgroundColor3,
    propBackgroundColor4,
    propColor,
    propColor1,
    propColor2,
    propColor3,
    propColor4,
    propMinWidth,
    propMinWidth1,
    propMinWidth2,
    propMinWidth3,
    propMinWidth4,
    component7Top,
    component7Top1,
    component7Top2,
    component7Top3,
    component7Top4,
    component7Padding,
    component7Padding1,
    component7Padding2,
    component7Padding3,
    component7Padding4,
    component7Height,
    component7Height1,
    component7Height2,
    component7Height3,
    component7Height4,
    component7Position,
    component7Position1,
    component7Position2,
    component7Position3,
    component7Position4,
    divDisplay,
    divDisplay1,
    divDisplay2,
    divDisplay3,
    divDisplay4,
  }) => {
    return (
      <div
        className={`self-stretch flex flex-row items-start justify-start flex-wrap content-start gap-x-[0.25rem] gap-y-[3.125rem] text-left text-[0.875rem] text-[#3c3c3c] font-[Inter] ${className}`}
      >
        <div className="border-[#c0bebe] border-b-[1px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[4.375rem] pr-[4.312rem] z-[3]">
          <div className="relative inline-block min-w-[2.688rem]">Огляд</div>
        </div>
        <div className="border-[#c0bebe] border-b-[1px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[3.937rem] pr-[3.875rem] z-[1]">
          <div className="relative inline-block min-w-[3.563rem]"> Відгуки</div>
        </div>
        <div className="border-[#009ea0] border-b-[2px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.5rem] pl-[3.187rem] pr-[3.125rem] z-[1]">
          <div className="relative inline-block min-w-[5.063rem]">
            Статистика
          </div>
        </div>
        <div className="border-[#c0bebe] border-b-[1px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[3.625rem] pr-[3.562rem] z-[1] text-[#000]">
          <div className="relative">Написати</div>
        </div>
        <div className="border-[#c0bebe] border-b-[1px] border-solid overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[2.187rem] pr-[2.125rem] z-[1]">
          <div className="relative inline-block min-w-[7.063rem]">
            Умови співпраці
          </div>
        </div>
        <Component5
          propLeft={propLeft}
          propBorder={propBorder}
          propBackgroundColor={propBackgroundColor}
          prop={prop}
          propColor={propColor}
          propMinWidth={propMinWidth}
          component7Top={component7Top}
          component7Padding={component7Padding}
          component7Height={component7Height}
          component7Position={component7Position}
          divDisplay={divDisplay}
        />
        <Component5
          propLeft={propLeft1}
          propBorder={propBorder1}
          propBackgroundColor={propBackgroundColor1}
          prop={prop1}
          propColor={propColor1}
          propMinWidth={propMinWidth1}
          component7Top={component7Top1}
          component7Padding={component7Padding1}
          component7Height={component7Height1}
          component7Position={component7Position1}
          divDisplay={divDisplay1}
        />
        <Component5
          propLeft={propLeft2}
          propBorder={propBorder2}
          propBackgroundColor={propBackgroundColor2}
          prop={prop2}
          propColor={propColor2}
          propMinWidth={propMinWidth2}
          component7Top={component7Top2}
          component7Padding={component7Padding2}
          component7Height={component7Height2}
          component7Position={component7Position2}
          divDisplay={divDisplay2}
        />
        <Component5
          propLeft={propLeft3}
          propBorder={propBorder3}
          propBackgroundColor={propBackgroundColor3}
          prop={prop3}
          propColor={propColor3}
          propMinWidth={propMinWidth3}
          component7Top={component7Top3}
          component7Padding={component7Padding3}
          component7Height={component7Height3}
          component7Position={component7Position3}
          divDisplay={divDisplay3}
        />
        <Component5
          propLeft={propLeft4}
          propBorder={propBorder4}
          propBackgroundColor={propBackgroundColor4}
          prop={prop4}
          propColor={propColor4}
          propMinWidth={propMinWidth4}
          component7Top={component7Top4}
          component7Padding={component7Padding4}
          component7Height={component7Height4}
          component7Position={component7Position4}
          divDisplay={divDisplay4}
        />
      </div>
    );
  }
);

export default ProfileNavigation;
