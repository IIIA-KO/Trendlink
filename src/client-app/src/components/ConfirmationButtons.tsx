import { FunctionComponent, useMemo, type CSSProperties } from "react";

export type ConfirmationButtonsType = {
  className?: string;

  /** Style props */
  propBorder?: CSSProperties["border"];
  propBorder1?: CSSProperties["border"];
};

const ConfirmationButtons: FunctionComponent<ConfirmationButtonsType> = ({
  className = "",
  propBorder,
  propBorder1,
}) => {
  const groupDivStyle: CSSProperties = useMemo(() => {
    return {
      border: propBorder,
    };
  }, [propBorder]);

  const rectangleDivStyle: CSSProperties = useMemo(() => {
    return {
      border: propBorder1,
    };
  }, [propBorder1]);

  return (
    <div
      className={`w-[180px] flex flex-col items-start justify-start pt-0.5 px-0 pb-0 box-border text-center text-5xl text-main-black font-inter ${className}`}
    >
      <div className="self-stretch flex flex-col items-end justify-start gap-[39px]">
        <img
          className="w-[25px] h-[25px] relative object-cover z-[2]"
          loading="lazy"
          alt=""
          src="/actions@2x.png"
        />
        <div
          className="self-stretch rounded-8xs bg-white flex flex-row items-start justify-center pt-3 px-5 pb-[13px] z-[2]"
          style={groupDivStyle}
        >
          <div
            className="h-[54px] w-[180px] relative rounded-8xs bg-white hidden"
            style={rectangleDivStyle}
          />
          <div className="w-4 relative font-light inline-block min-w-[16px] z-[1] mq450:text-lgi">
            3
          </div>
        </div>
      </div>
    </div>
  );
};

export default ConfirmationButtons;
