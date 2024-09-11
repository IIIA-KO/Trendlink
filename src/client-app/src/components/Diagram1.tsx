import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type Component1Type = {
  className?: string;

  /** Style props */
  propFlex?: CSSProperties["flex"];
  propPosition?: CSSProperties["position"];
  propTop?: CSSProperties["top"];
  propLeft?: CSSProperties["left"];
  propWidth?: CSSProperties["width"];
};

const Component1: FunctionComponent<Component1Type> = ({
  className = "",
  propFlex,
  propPosition,
  propTop,
  propLeft,
  propWidth,
}) => {
  const div4Style: CSSProperties = useMemo(() => {
    return {
      flex: propFlex,
      position: propPosition,
      top: propTop,
      left: propLeft,
      width: propWidth,
    };
  }, [propFlex, propPosition, propTop, propLeft, propWidth]);

  return (
    <div
      className={`h-[201px] flex-1 relative rounded-xl py-[50px] bg-aliceblue-200 max-w-full z-[1] text-right text-sm text-text font-inter ${className}`}
      style={div4Style}
    >
      <div className="absolute top-[0px] left-[0px] rounded-xl bg-aliceblue-200 w-full h-full hidden" />
      <div className="absolute top-[47px] left-[30px] rounded-3xs border-main-green border-[2px] border-solid box-border w-[120px] h-[115px] hidden" />
      <div className="absolute top-[30px] left-[47px] w-[375px] h-[142px]">
        <div className="absolute top-[0px] left-[80px] w-[248px] flex flex-row items-start justify-start gap-[22px]">
          <div className="w-[81px]  flex flex-col items-end justify-start gap-8">
            <b className="self-stretch relative inline-block min-w-[81px] z-[1]">
              $300-$700
            </b>
            <div className="w-[38px] flex flex-col items-end justify-start gap-[15px]">
              <b className="self-stretch relative inline-block min-w-[38px] z-[1]">
                2800
              </b>
              <b className="self-stretch relative inline-block min-w-[38px] z-[1]">
                3200
              </b>
              <b className="w-[29px] relative inline-block min-w-[29px] z-[1]">
                606
              </b>
            </div>
          </div>
          <div className="flex-1 flex flex-col items-start justify-start gap-[15px] text-left">
            <div className="self-stretch relative z-[1]">
              Середній діапазон цін на рекламу
            </div>
            <div className="relative inline-block min-w-[63px] z-[1]">{`Підписок `}</div>
            <div className="relative inline-block min-w-[74px] z-[1]">
              Публікацій
            </div>
            <div className="flex flex-col items-start justify-start">
              <div className="relative inline-block min-w-[46px] z-[1]">{`Лайків `}</div>
              <div className="flex flex-row items-start justify-start py-0 pl-px pr-0 mt-[-3px] text-xs text-text-2">
                <div className="relative font-light inline-block min-w-[98px] z-[1]">
                  на останній пост
                </div>
              </div>
            </div>
          </div>
        </div>
        <img
          className="absolute top-[55px] left-[0px] w-[30px] h-[30px] overflow-hidden z-[1]"
          alt=""
          src="/skilliconsinstagram.svg"
        />
      </div>
      <b className="absolute top-[88.2px] left-[77px] text-xl inline-block text-left min-w-[38px] z-[2] mq450:text-base">
        25к
      </b>
    </div>
  );
};

export default Component1;
