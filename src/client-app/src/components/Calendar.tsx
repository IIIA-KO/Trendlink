import { FunctionComponent, useMemo, type CSSProperties } from "react";
import Days from "./Days";

export type CalendarType = {
  className?: string;
  prop?: string;
  prop1?: string;
  prop2?: string;
  prop3?: string;
  prop4?: string;
  prop5?: string;
  prop6?: string;
  prop7?: string;
  prop8?: string;
  prop9?: string;
  prop10?: string;
  prop11?: string;
  prop12?: string;
  prop13?: string;
  prop14?: string;
  prop15?: string;
  prop16?: string;
  prop17?: string;
  prop18?: string;
  prop19?: string;
  prop20?: string;
  prop21?: string;
  prop22?: string;
  prop23?: string;
  prop24?: string;
  prop25?: string;
  prop26?: string;
  prop27?: string;
  prop28?: string;
  prop29?: string;
  prop30?: string;
  prop31?: string;
  prop32?: string;
  prop33?: string;
  prop34?: string;
  propWidth?: string;
  propWidth1?: string;
  propWidth2?: string;
  propWidth3?: string;
  propWidth4?: string;
  propWidth5?: string;
  propWidth6?: string;
  propWidth7?: string;
  propWidth8?: string;
  propWidth9?: string;
  propWidth10?: string;
  propWidth11?: string;
  propWidth12?: string;
  propWidth13?: string;
  propWidth14?: string;
  propWidth15?: string;
  propWidth16?: string;
  propWidth17?: string;
  propWidth18?: string;
  propWidth19?: string;
  propWidth20?: string;
  propWidth21?: string;
  propWidth22?: string;
  propWidth23?: string;
  propWidth24?: string;
  propWidth25?: string;
  propWidth26?: string;
  propWidth27?: string;
  propWidth28?: string;
  propWidth29?: string;
  propWidth30?: string;
  propWidth31?: string;
  propWidth32?: string;
  propWidth33?: string;
  propFlex?: string;
  propFlex1?: string;
  propFlex2?: string;
  propFlex3?: string;
  propFlex4?: string;
  propFlex5?: string;
  propFlex6?: string;
  propFlex7?: string;
  propFlex8?: string;
  propFlex9?: string;
  propFlex10?: string;
  propFlex11?: string;
  propFlex12?: string;
  propFlex13?: string;
  propFlex14?: string;
  propFlex15?: string;
  propFlex16?: string;
  propFlex17?: string;
  propFlex18?: string;
  propFlex19?: string;
  propFlex20?: string;
  propFlex21?: string;
  propFlex22?: string;
  propFlex23?: string;
  propFlex24?: string;
  propFlex25?: string;
  propFlex26?: string;
  propFlex27?: string;
  propFlex28?: string;
  propFlex29?: string;
  propFlex30?: string;
  propFlex31?: string;
  propFlex32?: string;
  propFlex33?: string;
  propBorderRadius?: string;
  propBorderRadius1?: string;
  propBorderRadius2?: string;
  propBorderRadius3?: string;
  propBorderRadius4?: string;
  propBorderRadius5?: string;
  propBorderRadius6?: string;
  propBorderRadius7?: string;
  propBorderRadius8?: string;
  propBorderRadius9?: string;
  propBorderRadius10?: string;
  propBorderRadius11?: string;
  propBorderRadius12?: string;
  propBorderRadius13?: string;
  propBorderRadius14?: string;
  propBorderRadius15?: string;
  propBorderRadius16?: string;
  propBorderRadius17?: string;
  propBorderRadius18?: string;
  propBorderRadius19?: string;
  propBorderRadius20?: string;
  propBorderRadius21?: string;
  propBorderRadius22?: string;
  propBorderRadius23?: string;
  propBorderRadius24?: string;
  propBorderRadius25?: string;
  propBorderRadius26?: string;
  propBorderRadius27?: string;
  propBorderRadius28?: string;
  propBorderRadius29?: string;
  propBorderRadius30?: string;
  propBorderRadius31?: string;
  propBorderRadius32?: string;
  propBorderRadius33?: string;
  propBackgroundColor?: string;
  propBackgroundColor1?: string;
  propBackgroundColor2?: string;
  propBackgroundColor3?: string;
  propBackgroundColor4?: string;
  propBackgroundColor5?: string;
  propBackgroundColor6?: string;
  propBackgroundColor7?: string;
  propBackgroundColor8?: string;
  propBackgroundColor9?: string;
  propBackgroundColor10?: string;
  propBackgroundColor11?: string;
  propBackgroundColor12?: string;
  propBackgroundColor13?: string;
  propBackgroundColor14?: string;
  propBackgroundColor15?: string;
  propBackgroundColor16?: string;
  propBackgroundColor17?: string;
  propBackgroundColor18?: string;
  propBackgroundColor19?: string;
  propBackgroundColor20?: string;
  propBackgroundColor21?: string;
  propBackgroundColor22?: string;
  propBackgroundColor23?: string;
  propBackgroundColor24?: string;
  propBackgroundColor25?: string;
  propBackgroundColor26?: string;
  propBackgroundColor27?: string;
  propBackgroundColor28?: string;
  propBackgroundColor29?: string;
  propBackgroundColor30?: string;
  propBackgroundColor31?: string;
  propBackgroundColor32?: string;
  propBackgroundColor33?: string;
  propOverflow?: string;
  propOverflow1?: string;
  propOverflow2?: string;
  propOverflow3?: string;
  propOverflow4?: string;
  propOverflow5?: string;
  propOverflow6?: string;
  propOverflow7?: string;
  propOverflow8?: string;
  propOverflow9?: string;
  propOverflow10?: string;
  propOverflow11?: string;
  propOverflow12?: string;
  propOverflow13?: string;
  propOverflow14?: string;
  propOverflow15?: string;
  propOverflow16?: string;
  propOverflow17?: string;
  propOverflow18?: string;
  propOverflow19?: string;
  propOverflow20?: string;
  propOverflow21?: string;
  propOverflow22?: string;
  propOverflow23?: string;
  propOverflow24?: string;
  propOverflow25?: string;
  propOverflow26?: string;
  propOverflow27?: string;
  propOverflow28?: string;
  propOverflow29?: string;
  propOverflow30?: string;
  propOverflow31?: string;
  propOverflow32?: string;
  propOverflow33?: string;
  propColor?: string;
  propColor1?: string;
  propColor2?: string;
  propColor3?: string;
  propColor4?: string;
  propColor5?: string;
  propColor6?: string;
  propColor7?: string;
  propColor8?: string;
  propColor9?: string;
  propColor10?: string;
  propColor11?: string;
  propColor12?: string;
  propColor13?: string;
  propColor14?: string;
  propColor15?: string;
  propColor16?: string;
  propColor17?: string;
  propColor18?: string;
  propColor19?: string;
  propColor20?: string;
  propColor21?: string;
  propColor22?: string;
  propColor23?: string;
  propColor24?: string;
  propColor25?: string;
  propColor26?: string;
  propColor27?: string;
  propColor28?: string;
  propColor29?: string;
  propColor30?: string;
  propColor31?: string;
  propColor32?: string;
  propColor33?: string;
  propTextShadow?: string;
  propTextShadow1?: string;
  propTextShadow2?: string;
  propTextShadow3?: string;
  propTextShadow4?: string;
  propTextShadow5?: string;
  propTextShadow6?: string;
  propTextShadow7?: string;
  propTextShadow8?: string;
  propTextShadow9?: string;
  propTextShadow10?: string;
  propTextShadow11?: string;
  propTextShadow12?: string;
  propTextShadow13?: string;
  propTextShadow14?: string;
  propTextShadow15?: string;
  propTextShadow16?: string;
  propTextShadow17?: string;
  propTextShadow18?: string;
  propTextShadow19?: string;
  propTextShadow20?: string;
  propTextShadow21?: string;
  propTextShadow22?: string;
  propTextShadow23?: string;
  propTextShadow24?: string;
  propTextShadow25?: string;
  propTextShadow26?: string;
  propTextShadow27?: string;
  propTextShadow28?: string;
  propTextShadow29?: string;
  propTextShadow30?: string;
  propTextShadow31?: string;
  propTextShadow32?: string;
  propTextShadow33?: string;
  propTextDecoration?: string;
  propTextDecoration1?: string;
  propTextDecoration2?: string;
  propTextDecoration3?: string;
  propTextDecoration4?: string;
  propTextDecoration5?: string;
  propTextDecoration6?: string;
  propTextDecoration7?: string;
  propTextDecoration8?: string;
  propTextDecoration9?: string;
  propTextDecoration10?: string;
  propTextDecoration11?: string;
  propTextDecoration12?: string;
  propTextDecoration13?: string;
  propTextDecoration14?: string;
  propTextDecoration15?: string;
  propTextDecoration16?: string;
  propTextDecoration17?: string;
  propTextDecoration18?: string;
  propTextDecoration19?: string;
  propTextDecoration20?: string;
  propTextDecoration21?: string;
  propTextDecoration22?: string;
  propTextDecoration23?: string;
  propTextDecoration24?: string;
  propTextDecoration25?: string;
  propTextDecoration26?: string;
  propTextDecoration27?: string;
  propTextDecoration28?: string;
  propTextDecoration29?: string;
  propTextDecoration30?: string;
  propTextDecoration31?: string;
  propTextDecoration32?: string;
  propTextDecoration33?: string;

  /** Style props */
  propMinWidth?: CSSProperties["minWidth"];
  propMinWidth1?: CSSProperties["minWidth"];
  propMinWidth2?: CSSProperties["minWidth"];
  propMinWidth3?: CSSProperties["minWidth"];
  propMinWidth4?: CSSProperties["minWidth"];
  propMinWidth5?: CSSProperties["minWidth"];
  propMinWidth6?: CSSProperties["minWidth"];
  propMinWidth7?: CSSProperties["minWidth"];
};

const Calendar: FunctionComponent<CalendarType> = ({
  className = "",
  propMinWidth,
  prop,
  propMinWidth1,
  propMinWidth2,
  propMinWidth3,
  propMinWidth4,
  propMinWidth5,
  propMinWidth6,
  propMinWidth7,
  prop1,
  prop2,
  prop3,
  prop4,
  prop5,
  prop6,
  prop7,
  prop8,
  prop9,
  prop10,
  prop11,
  prop12,
  prop13,
  prop14,
  prop15,
  prop16,
  prop17,
  prop18,
  prop19,
  prop20,
  prop21,
  prop22,
  prop23,
  prop24,
  prop25,
  prop26,
  prop27,
  prop28,
  prop29,
  prop30,
  prop31,
  prop32,
  prop33,
  prop34,
  propWidth,
  propWidth1,
  propWidth2,
  propWidth3,
  propWidth4,
  propWidth5,
  propWidth6,
  propWidth7,
  propWidth8,
  propWidth9,
  propWidth10,
  propWidth11,
  propWidth12,
  propWidth13,
  propWidth14,
  propWidth15,
  propWidth16,
  propWidth17,
  propWidth18,
  propWidth19,
  propWidth20,
  propWidth21,
  propWidth22,
  propWidth23,
  propWidth24,
  propWidth25,
  propWidth26,
  propWidth27,
  propWidth28,
  propWidth29,
  propWidth30,
  propWidth31,
  propWidth32,
  propWidth33,
  propFlex,
  propFlex1,
  propFlex2,
  propFlex3,
  propFlex4,
  propFlex5,
  propFlex6,
  propFlex7,
  propFlex8,
  propFlex9,
  propFlex10,
  propFlex11,
  propFlex12,
  propFlex13,
  propFlex14,
  propFlex15,
  propFlex16,
  propFlex17,
  propFlex18,
  propFlex19,
  propFlex20,
  propFlex21,
  propFlex22,
  propFlex23,
  propFlex24,
  propFlex25,
  propFlex26,
  propFlex27,
  propFlex28,
  propFlex29,
  propFlex30,
  propFlex31,
  propFlex32,
  propFlex33,
  propBorderRadius,
  propBorderRadius1,
  propBorderRadius2,
  propBorderRadius3,
  propBorderRadius4,
  propBorderRadius5,
  propBorderRadius6,
  propBorderRadius7,
  propBorderRadius8,
  propBorderRadius9,
  propBorderRadius10,
  propBorderRadius11,
  propBorderRadius12,
  propBorderRadius13,
  propBorderRadius14,
  propBorderRadius15,
  propBorderRadius16,
  propBorderRadius17,
  propBorderRadius18,
  propBorderRadius19,
  propBorderRadius20,
  propBorderRadius21,
  propBorderRadius22,
  propBorderRadius23,
  propBorderRadius24,
  propBorderRadius25,
  propBorderRadius26,
  propBorderRadius27,
  propBorderRadius28,
  propBorderRadius29,
  propBorderRadius30,
  propBorderRadius31,
  propBorderRadius32,
  propBorderRadius33,
  propBackgroundColor,
  propBackgroundColor1,
  propBackgroundColor2,
  propBackgroundColor3,
  propBackgroundColor4,
  propBackgroundColor5,
  propBackgroundColor6,
  propBackgroundColor7,
  propBackgroundColor8,
  propBackgroundColor9,
  propBackgroundColor10,
  propBackgroundColor11,
  propBackgroundColor12,
  propBackgroundColor13,
  propBackgroundColor14,
  propBackgroundColor15,
  propBackgroundColor16,
  propBackgroundColor17,
  propBackgroundColor18,
  propBackgroundColor19,
  propBackgroundColor20,
  propBackgroundColor21,
  propBackgroundColor22,
  propBackgroundColor23,
  propBackgroundColor24,
  propBackgroundColor25,
  propBackgroundColor26,
  propBackgroundColor27,
  propBackgroundColor28,
  propBackgroundColor29,
  propBackgroundColor30,
  propBackgroundColor31,
  propBackgroundColor32,
  propBackgroundColor33,
  propOverflow,
  propOverflow1,
  propOverflow2,
  propOverflow3,
  propOverflow4,
  propOverflow5,
  propOverflow6,
  propOverflow7,
  propOverflow8,
  propOverflow9,
  propOverflow10,
  propOverflow11,
  propOverflow12,
  propOverflow13,
  propOverflow14,
  propOverflow15,
  propOverflow16,
  propOverflow17,
  propOverflow18,
  propOverflow19,
  propOverflow20,
  propOverflow21,
  propOverflow22,
  propOverflow23,
  propOverflow24,
  propOverflow25,
  propOverflow26,
  propOverflow27,
  propOverflow28,
  propOverflow29,
  propOverflow30,
  propOverflow31,
  propOverflow32,
  propOverflow33,
  propColor,
  propColor1,
  propColor2,
  propColor3,
  propColor4,
  propColor5,
  propColor6,
  propColor7,
  propColor8,
  propColor9,
  propColor10,
  propColor11,
  propColor12,
  propColor13,
  propColor14,
  propColor15,
  propColor16,
  propColor17,
  propColor18,
  propColor19,
  propColor20,
  propColor21,
  propColor22,
  propColor23,
  propColor24,
  propColor25,
  propColor26,
  propColor27,
  propColor28,
  propColor29,
  propColor30,
  propColor31,
  propColor32,
  propColor33,
  propTextShadow,
  propTextShadow1,
  propTextShadow2,
  propTextShadow3,
  propTextShadow4,
  propTextShadow5,
  propTextShadow6,
  propTextShadow7,
  propTextShadow8,
  propTextShadow9,
  propTextShadow10,
  propTextShadow11,
  propTextShadow12,
  propTextShadow13,
  propTextShadow14,
  propTextShadow15,
  propTextShadow16,
  propTextShadow17,
  propTextShadow18,
  propTextShadow19,
  propTextShadow20,
  propTextShadow21,
  propTextShadow22,
  propTextShadow23,
  propTextShadow24,
  propTextShadow25,
  propTextShadow26,
  propTextShadow27,
  propTextShadow28,
  propTextShadow29,
  propTextShadow30,
  propTextShadow31,
  propTextShadow32,
  propTextShadow33,
  propTextDecoration,
  propTextDecoration1,
  propTextDecoration2,
  propTextDecoration3,
  propTextDecoration4,
  propTextDecoration5,
  propTextDecoration6,
  propTextDecoration7,
  propTextDecoration8,
  propTextDecoration9,
  propTextDecoration10,
  propTextDecoration11,
  propTextDecoration12,
  propTextDecoration13,
  propTextDecoration14,
  propTextDecoration15,
  propTextDecoration16,
  propTextDecoration17,
  propTextDecoration18,
  propTextDecoration19,
  propTextDecoration20,
  propTextDecoration21,
  propTextDecoration22,
  propTextDecoration23,
  propTextDecoration24,
  propTextDecoration25,
  propTextDecoration26,
  propTextDecoration27,
  propTextDecoration28,
  propTextDecoration29,
  propTextDecoration30,
  propTextDecoration31,
  propTextDecoration32,
  propTextDecoration33,
}) => {
  const calendarStyle: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth,
    };
  }, [propMinWidth]);

  const weekBaseStyle: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth1,
    };
  }, [propMinWidth1]);

  const weekBase1Style: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth2,
    };
  }, [propMinWidth2]);

  const weekBase2Style: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth3,
    };
  }, [propMinWidth3]);

  const weekBase3Style: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth4,
    };
  }, [propMinWidth4]);

  const weekBase4Style: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth5,
    };
  }, [propMinWidth5]);

  const weekBase5Style: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth6,
    };
  }, [propMinWidth6]);

  const weekBase6Style: CSSProperties = useMemo(() => {
    return {
      minWidth: propMinWidth7,
    };
  }, [propMinWidth7]);

  return (
    <div
      className={`w-[298px] shadow-[2px_2px_10px_rgba(150,_150,_150,_0.11)] rounded-xl bg-aliceblue-100 overflow-hidden shrink-0 flex flex-col items-start justify-start py-6 px-0 box-border gap-4 z-[1] text-center text-sm text-main-black font-inter ${className}`}
      style={calendarStyle}
    >
      <div className="ml-[-5px] w-[308px] flex flex-col items-start justify-start gap-5">
        <div className="self-stretch flex flex-row items-start justify-start py-0 px-6 mq450:gap-[51px]">
          <div className="flex-1 relative tracking-[0.01em]">
            <span className="uppercase">{prop}</span> 2024
          </div>
        </div>
        <div className="self-stretch flex flex-col items-start justify-start gap-[15.2px] text-3xs text-slategray">
          <div className="self-stretch flex flex-row items-start justify-start py-0 px-7">
            <div className="h-[0.8px] flex-1 relative border-neutral-blue-black-40 border-t-[0.8px] border-solid box-border" />
          </div>
          <div className="self-stretch flex flex-row items-start justify-start py-0 px-6 gap-1.5 mq450:flex-wrap">
            <div
              className="flex-1 flex flex-row items-start justify-start p-1 box-border min-w-[31px] max-w-[32px]"
              style={weekBaseStyle}
            >
              <div className="flex-1 relative uppercase">пн</div>
            </div>
            <div
              className="flex-1 flex flex-row items-start justify-start p-1 box-border min-w-[31px] max-w-[32px]"
              style={weekBase1Style}
            >
              <div className="flex-1 relative uppercase">вт</div>
            </div>
            <div
              className="flex-1 flex flex-row items-start justify-start p-1 box-border min-w-[31px] max-w-[32px]"
              style={weekBase2Style}
            >
              <div className="flex-1 relative uppercase">ср</div>
            </div>
            <div
              className="flex-1 flex flex-row items-start justify-start p-1 box-border min-w-[31px] max-w-[32px]"
              style={weekBase3Style}
            >
              <div className="flex-1 relative uppercase">чт</div>
            </div>
            <div
              className="flex-1 flex flex-row items-start justify-start p-1 box-border min-w-[31px] max-w-[32px]"
              style={weekBase4Style}
            >
              <div className="flex-1 relative uppercase">пт</div>
            </div>
            <div
              className="flex-1 flex flex-row items-start justify-start p-1 box-border min-w-[31px] max-w-[32px]"
              style={weekBase5Style}
            >
              <div className="flex-1 relative uppercase">сб</div>
            </div>
            <div
              className="flex-1 flex flex-row items-start justify-start p-1 box-border min-w-[31px] max-w-[32px]"
              style={weekBase6Style}
            >
              <div className="flex-1 relative uppercase">нд</div>
            </div>
          </div>
        </div>
      </div>
      <div className="ml-[-5px] w-[308px] flex flex-col items-start justify-start gap-4 text-darkslategray-200">
        <div className="self-stretch flex flex-row items-start justify-start py-0 px-6 gap-[15.3px] mq450:flex-wrap">
          <Days
            propWidth={propWidth}
            propFlex={propFlex}
            propBorderRadius={propBorderRadius}
            propBackgroundColor={propBackgroundColor}
            propOverflow={propOverflow}
            prop={prop1}
            propColor={propColor}
            propTextShadow={propTextShadow}
            propTextDecoration={propTextDecoration}
          />
          <Days
            propWidth={propWidth1}
            propFlex={propFlex1}
            propBorderRadius={propBorderRadius1}
            propBackgroundColor={propBackgroundColor1}
            propOverflow={propOverflow1}
            prop={prop2}
            propColor={propColor1}
            propTextShadow={propTextShadow1}
            propTextDecoration={propTextDecoration1}
          />
          <Days
            propWidth={propWidth2}
            propFlex={propFlex2}
            propBorderRadius={propBorderRadius2}
            propBackgroundColor={propBackgroundColor2}
            propOverflow={propOverflow2}
            prop={prop3}
            propColor={propColor2}
            propTextShadow={propTextShadow2}
            propTextDecoration={propTextDecoration2}
          />
          <Days
            propWidth={propWidth3}
            propFlex={propFlex3}
            propBorderRadius={propBorderRadius3}
            propBackgroundColor={propBackgroundColor3}
            propOverflow={propOverflow3}
            prop={prop4}
            propColor={propColor3}
            propTextShadow={propTextShadow3}
            propTextDecoration={propTextDecoration3}
          />
          <Days
            propWidth={propWidth4}
            propFlex={propFlex4}
            propBorderRadius={propBorderRadius4}
            propBackgroundColor={propBackgroundColor4}
            propOverflow={propOverflow4}
            prop={prop5}
            propColor={propColor4}
            propTextShadow={propTextShadow4}
            propTextDecoration={propTextDecoration4}
          />
          <Days
            propWidth={propWidth5}
            propFlex={propFlex5}
            propBorderRadius={propBorderRadius5}
            propBackgroundColor={propBackgroundColor5}
            propOverflow={propOverflow5}
            prop={prop6}
            propColor={propColor5}
            propTextShadow={propTextShadow5}
            propTextDecoration={propTextDecoration5}
          />
          <Days
            propWidth={propWidth6}
            propFlex={propFlex6}
            propBorderRadius={propBorderRadius6}
            propBackgroundColor={propBackgroundColor6}
            propOverflow={propOverflow6}
            prop={prop7}
            propColor={propColor6}
            propTextShadow={propTextShadow6}
            propTextDecoration={propTextDecoration6}
          />
        </div>
        <div className="self-stretch flex flex-row items-start justify-start py-0 px-6 gap-[15.3px] mq450:flex-wrap">
          <Days
            propWidth={propWidth7}
            propFlex={propFlex7}
            propBorderRadius={propBorderRadius7}
            propBackgroundColor={propBackgroundColor7}
            propOverflow={propOverflow7}
            prop={prop8}
            propColor={propColor7}
            propTextShadow={propTextShadow7}
            propTextDecoration={propTextDecoration7}
          />
          <Days
            propWidth={propWidth8}
            propFlex={propFlex8}
            propBorderRadius={propBorderRadius8}
            propBackgroundColor={propBackgroundColor8}
            propOverflow={propOverflow8}
            prop={prop9}
            propColor={propColor8}
            propTextShadow={propTextShadow8}
            propTextDecoration={propTextDecoration8}
          />
          <Days
            propWidth={propWidth9}
            propFlex={propFlex9}
            propBorderRadius={propBorderRadius9}
            propBackgroundColor={propBackgroundColor9}
            propOverflow={propOverflow9}
            prop={prop10}
            propColor={propColor9}
            propTextShadow={propTextShadow9}
            propTextDecoration={propTextDecoration9}
          />
          <Days
            propWidth={propWidth10}
            propFlex={propFlex10}
            propBorderRadius={propBorderRadius10}
            propBackgroundColor={propBackgroundColor10}
            propOverflow={propOverflow10}
            prop={prop11}
            propColor={propColor10}
            propTextShadow={propTextShadow10}
            propTextDecoration={propTextDecoration10}
          />
          <Days
            propWidth={propWidth11}
            propFlex={propFlex11}
            propBorderRadius={propBorderRadius11}
            propBackgroundColor={propBackgroundColor11}
            propOverflow={propOverflow11}
            prop={prop12}
            propColor={propColor11}
            propTextShadow={propTextShadow11}
            propTextDecoration={propTextDecoration11}
          />
          <Days
            propWidth={propWidth12}
            propFlex={propFlex12}
            propBorderRadius={propBorderRadius12}
            propBackgroundColor={propBackgroundColor12}
            propOverflow={propOverflow12}
            prop={prop13}
            propColor={propColor12}
            propTextShadow={propTextShadow12}
            propTextDecoration={propTextDecoration12}
          />
          <Days
            propWidth={propWidth13}
            propFlex={propFlex13}
            propBorderRadius={propBorderRadius13}
            propBackgroundColor={propBackgroundColor13}
            propOverflow={propOverflow13}
            prop={prop14}
            propColor={propColor13}
            propTextShadow={propTextShadow13}
            propTextDecoration={propTextDecoration13}
          />
        </div>
        <div className="self-stretch flex flex-row items-start justify-start py-0 px-6 gap-[15.3px] mq450:flex-wrap">
          <Days
            propWidth={propWidth14}
            propFlex={propFlex14}
            propBorderRadius={propBorderRadius14}
            propBackgroundColor={propBackgroundColor14}
            propOverflow={propOverflow14}
            prop={prop15}
            propColor={propColor14}
            propTextShadow={propTextShadow14}
            propTextDecoration={propTextDecoration14}
          />
          <Days
            propWidth={propWidth15}
            propFlex={propFlex15}
            propBorderRadius={propBorderRadius15}
            propBackgroundColor={propBackgroundColor15}
            propOverflow={propOverflow15}
            prop={prop16}
            propColor={propColor15}
            propTextShadow={propTextShadow15}
            propTextDecoration={propTextDecoration15}
          />
          <Days
            propWidth={propWidth16}
            propFlex={propFlex16}
            propBorderRadius={propBorderRadius16}
            propBackgroundColor={propBackgroundColor16}
            propOverflow={propOverflow16}
            prop={prop17}
            propColor={propColor16}
            propTextShadow={propTextShadow16}
            propTextDecoration={propTextDecoration16}
          />
          <Days
            propWidth={propWidth17}
            propFlex={propFlex17}
            propBorderRadius={propBorderRadius17}
            propBackgroundColor={propBackgroundColor17}
            propOverflow={propOverflow17}
            prop={prop18}
            propColor={propColor17}
            propTextShadow={propTextShadow17}
            propTextDecoration={propTextDecoration17}
          />
          <Days
            propWidth={propWidth18}
            propFlex={propFlex18}
            propBorderRadius={propBorderRadius18}
            propBackgroundColor={propBackgroundColor18}
            propOverflow={propOverflow18}
            prop={prop19}
            propColor={propColor18}
            propTextShadow={propTextShadow18}
            propTextDecoration={propTextDecoration18}
          />
          <Days
            propWidth={propWidth19}
            propFlex={propFlex19}
            propBorderRadius={propBorderRadius19}
            propBackgroundColor={propBackgroundColor19}
            propOverflow={propOverflow19}
            prop={prop20}
            propColor={propColor19}
            propTextShadow={propTextShadow19}
            propTextDecoration={propTextDecoration19}
          />
          <Days
            propWidth={propWidth20}
            propFlex={propFlex20}
            propBorderRadius={propBorderRadius20}
            propBackgroundColor={propBackgroundColor20}
            propOverflow={propOverflow20}
            prop={prop21}
            propColor={propColor20}
            propTextShadow={propTextShadow20}
            propTextDecoration={propTextDecoration20}
          />
        </div>
        <div className="self-stretch flex flex-row items-start justify-start py-0 px-6 gap-[15.3px] mq450:flex-wrap">
          <Days
            propWidth={propWidth21}
            propFlex={propFlex21}
            propBorderRadius={propBorderRadius21}
            propBackgroundColor={propBackgroundColor21}
            propOverflow={propOverflow21}
            prop={prop22}
            propColor={propColor21}
            propTextShadow={propTextShadow21}
            propTextDecoration={propTextDecoration21}
          />
          <Days
            propWidth={propWidth22}
            propFlex={propFlex22}
            propBorderRadius={propBorderRadius22}
            propBackgroundColor={propBackgroundColor22}
            propOverflow={propOverflow22}
            prop={prop23}
            propColor={propColor22}
            propTextShadow={propTextShadow22}
            propTextDecoration={propTextDecoration22}
          />
          <Days
            propWidth={propWidth23}
            propFlex={propFlex23}
            propBorderRadius={propBorderRadius23}
            propBackgroundColor={propBackgroundColor23}
            propOverflow={propOverflow23}
            prop={prop24}
            propColor={propColor23}
            propTextShadow={propTextShadow23}
            propTextDecoration={propTextDecoration23}
          />
          <Days
            propWidth={propWidth24}
            propFlex={propFlex24}
            propBorderRadius={propBorderRadius24}
            propBackgroundColor={propBackgroundColor24}
            propOverflow={propOverflow24}
            prop={prop25}
            propColor={propColor24}
            propTextShadow={propTextShadow24}
            propTextDecoration={propTextDecoration24}
          />
          <Days
            propWidth={propWidth25}
            propFlex={propFlex25}
            propBorderRadius={propBorderRadius25}
            propBackgroundColor={propBackgroundColor25}
            propOverflow={propOverflow25}
            prop={prop26}
            propColor={propColor25}
            propTextShadow={propTextShadow25}
            propTextDecoration={propTextDecoration25}
          />
          <Days
            propWidth={propWidth26}
            propFlex={propFlex26}
            propBorderRadius={propBorderRadius26}
            propBackgroundColor={propBackgroundColor26}
            propOverflow={propOverflow26}
            prop={prop27}
            propColor={propColor26}
            propTextShadow={propTextShadow26}
            propTextDecoration={propTextDecoration26}
          />
          <Days
            propWidth={propWidth27}
            propFlex={propFlex27}
            propBorderRadius={propBorderRadius27}
            propBackgroundColor={propBackgroundColor27}
            propOverflow={propOverflow27}
            prop={prop28}
            propColor={propColor27}
            propTextShadow={propTextShadow27}
            propTextDecoration={propTextDecoration27}
          />
        </div>
        <div className="self-stretch flex flex-row items-start justify-between py-0 px-6 gap-5 mq450:flex-wrap">
          <Days
            propWidth={propWidth28}
            propFlex={propFlex28}
            propBorderRadius={propBorderRadius28}
            propBackgroundColor={propBackgroundColor28}
            propOverflow={propOverflow28}
            prop={prop29}
            propColor={propColor28}
            propTextShadow={propTextShadow28}
            propTextDecoration={propTextDecoration28}
          />
          <Days
            propWidth={propWidth29}
            propFlex={propFlex29}
            propBorderRadius={propBorderRadius29}
            propBackgroundColor={propBackgroundColor29}
            propOverflow={propOverflow29}
            prop={prop30}
            propColor={propColor29}
            propTextShadow={propTextShadow29}
            propTextDecoration={propTextDecoration29}
          />
          <Days
            propWidth={propWidth30}
            propFlex={propFlex30}
            propBorderRadius={propBorderRadius30}
            propBackgroundColor={propBackgroundColor30}
            propOverflow={propOverflow30}
            prop={prop31}
            propColor={propColor30}
            propTextShadow={propTextShadow30}
            propTextDecoration={propTextDecoration30}
          />
          <Days
            propWidth={propWidth31}
            propFlex={propFlex31}
            propBorderRadius={propBorderRadius31}
            propBackgroundColor={propBackgroundColor31}
            propOverflow={propOverflow31}
            prop={prop32}
            propColor={propColor31}
            propTextShadow={propTextShadow31}
            propTextDecoration={propTextDecoration31}
          />
          <Days
            propWidth={propWidth32}
            propFlex={propFlex32}
            propBorderRadius={propBorderRadius32}
            propBackgroundColor={propBackgroundColor32}
            propOverflow={propOverflow32}
            prop={prop33}
            propColor={propColor32}
            propTextShadow={propTextShadow32}
            propTextDecoration={propTextDecoration32}
          />
          <Days
            propWidth={propWidth33}
            propFlex={propFlex33}
            propBorderRadius={propBorderRadius33}
            propBackgroundColor={propBackgroundColor33}
            propOverflow={propOverflow33}
            prop={prop34}
            propColor={propColor33}
            propTextShadow={propTextShadow33}
            propTextDecoration={propTextDecoration33}
          />
        </div>
      </div>
    </div>
  );
};

export default Calendar;
