import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type Component2Type = {
  className?: string;
  group107?: string;

  /** Style props */
  propFlex?: CSSProperties["flex"];
  propMinWidth?: CSSProperties["minWidth"];
  propPosition?: CSSProperties["position"];
  propTop?: CSSProperties["top"];
  propLeft?: CSSProperties["left"];
  propFlex1?: CSSProperties["flex"];
  propMinWidth1?: CSSProperties["minWidth"];
};

const Component2: FunctionComponent<Component2Type> = ({
  className = "",
  propFlex,
  propMinWidth,
  propPosition,
  propTop,
  propLeft,
  propFlex1,
  propMinWidth1,
  group107,
}) => {
  const div5Style: CSSProperties = useMemo(() => {
    return {
      flex: propFlex,
      minWidth: propMinWidth,
      position: propPosition,
      top: propTop,
      left: propLeft,
    };
  }, [propFlex, propMinWidth, propPosition, propTop, propLeft]);

  const genderChartStyle: CSSProperties = useMemo(() => {
    return {
      flex: propFlex1,
      minWidth: propMinWidth1,
    };
  }, [propFlex1, propMinWidth1]);

  return (
    <div
      className={`flex-[0.6689] rounded-xl bg-aliceblue-200 flex flex-row items-start justify-start py-[50px] px-[74px] box-border gap-1 min-w-[291px] min-h-[201px] max-w-full z-[1] text-left text-sm text-text font-inter mq750:flex-wrap mq1050:flex-1 mq1050:min-h-[auto] mq450:pl-5 mq450:pr-5 mq450:box-border ${className}`}
      style={div5Style}
    >
      <div className="h-[201px] w-[447px] relative rounded-xl bg-aliceblue-200 hidden max-w-full" />
      <div className="flex flex-col items-start justify-start gap-[88px]">
        <b className="relative inline-block min-w-[42px] z-[1]">Стать</b>
        <div className="relative text-main-black inline-block min-w-[76px] z-[1]">
          Жінки 85%
        </div>
      </div>
      <div
        className="flex-1 flex flex-col items-start justify-start pt-3.5 pb-0 pl-0 pr-[5px] box-border min-w-[73px]"
        style={genderChartStyle}
      >
        <img
          className="w-[108px] h-[108px] relative z-[1]"
          loading="lazy"
          alt=""
          src={group107}
        />
      </div>
      <div className="flex flex-col items-start justify-start pt-[105px] px-0 pb-0 text-main-black">
        <div className="relative inline-block min-w-[94px] left-[20px] z-[1]">
          Чоловіки 15%
        </div>
      </div>
    </div>
  );
};

export default Component2;
