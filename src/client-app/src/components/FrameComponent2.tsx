import { FunctionComponent, memo, useMemo, type CSSProperties } from "react";

export type FrameComponent2Type = {
  className?: string;
  bG?: string;

  /** Style props */
  propTop?: CSSProperties["top"];
  propHeight?: CSSProperties["height"];
  propTop1?: CSSProperties["top"];
  propTop2?: CSSProperties["top"];
  propWidth?: CSSProperties["width"];
  propDisplay?: CSSProperties["display"];
  propMinWidth?: CSSProperties["minWidth"];
  propDisplay1?: CSSProperties["display"];
  propMinWidth1?: CSSProperties["minWidth"];
  propMinWidth2?: CSSProperties["minWidth"];
};

const FrameComponent2: FunctionComponent<FrameComponent2Type> = memo(
  ({
    className = "",
    propTop,
    propHeight,
    propTop1,
    bG,
    propTop2,
    propWidth,
    propDisplay,
    propMinWidth,
    propDisplay1,
    propMinWidth1,
    propMinWidth2,
  }) => {
    const frameDivStyle: CSSProperties = useMemo(() => {
      return {
        top: propTop,
      };
    }, [propTop]);

    const resourceHeaderStyle: CSSProperties = useMemo(() => {
      return {
        height: propHeight,
        top: propTop1,
      };
    }, [propHeight, propTop1]);

    const icon10Style: CSSProperties = useMemo(() => {
      return {
        top: propTop2,
      };
    }, [propTop2]);

    const metricLabelsStyle: CSSProperties = useMemo(() => {
      return {
        width: propWidth,
      };
    }, [propWidth]);

    const bStyle: CSSProperties = useMemo(() => {
      return {
        display: propDisplay,
        minWidth: propMinWidth,
      };
    }, [propDisplay, propMinWidth]);

    const kStyle: CSSProperties = useMemo(() => {
      return {
        display: propDisplay1,
        minWidth: propMinWidth1,
      };
    }, [propDisplay1, propMinWidth1]);

    const b1Style: CSSProperties = useMemo(() => {
      return {
        minWidth: propMinWidth2,
      };
    }, [propMinWidth2]);

    return (
      <div
        className={`absolute top-[24.875rem] left-[51.375rem] w-[18.688rem] flex flex-row items-start justify-start text-left text-[1rem] text-[#3c3c3c] font-[Inter] ${className}`}
        style={frameDivStyle}
      >
        <div
          className="h-[73.125rem] w-[63.5rem] absolute !m-[0] top-[-34.75rem] left-[-42.062rem]"
          style={resourceHeaderStyle}
        >
          <img
            className="absolute top-[0rem] left-[0rem] rounded-[40px] w-full h-full"
            alt=""
            src={bG}
          />
          <img
            className="absolute top-[32.563rem] left-[53.938rem] w-[0.938rem] h-[0.938rem] overflow-hidden z-[1]"
            loading="lazy"
            alt=""
            src="/-2-2.svg"
            style={icon10Style}
          />
        </div>
        <div className="flex-1 rounded-[20px] bg-[#f0f4f9] flex flex-row items-start justify-start pt-[7.187rem] px-[2.625rem] pb-[7.125rem] z-[1]">
          <div className="h-[31.813rem] w-[18.688rem] relative rounded-[20px] bg-[#f0f4f9] hidden" />
          <div className="flex-1 flex flex-col items-start justify-start gap-[2.5rem] z-[2]">
            <div className="self-stretch flex flex-col items-start justify-start gap-[0.25rem]">
              <b className="self-stretch relative">0.79%</b>
              <div className="relative text-[0.875rem]">
                Середній коефіцієнт залучення
              </div>
            </div>
            <div
              className="flex flex-col items-start justify-start gap-[0.25rem]"
              style={metricLabelsStyle}
            >
              <b className="relative" style={bStyle}>
                $0.93
              </b>
              <div className="self-stretch relative text-[0.875rem]">CPE</div>
            </div>
            <div className="flex flex-col items-start justify-start gap-[0.25rem]">
              <div className="flex flex-row items-center justify-start gap-[0.312rem]">
                <b className="relative" style={kStyle}>
                  1.5k
                </b>
                <img
                  className="h-[0.938rem] w-[0.938rem] relative object-cover"
                  loading="lazy"
                  alt=""
                  src="/actions--toggle--favorite--24@2x.png"
                />
              </div>
              <div className="relative text-[0.875rem]">
                Середня кількість лайків
              </div>
            </div>
            <div className="flex flex-col items-start justify-start gap-[0.25rem]">
              <div className="flex flex-row items-center justify-start gap-[0.312rem]">
                <b
                  className="relative inline-block min-w-[1.938rem]"
                  style={b1Style}
                >
                  814
                </b>
                <img
                  className="h-[0.938rem] w-[0.938rem] relative object-cover"
                  loading="lazy"
                  alt=""
                  src="/actions--operations--chat--24@2x.png"
                />
              </div>
              <div className="relative text-[0.875rem] tracking-[-0.01px]">
                Середня кількість коментарів
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
);

export default FrameComponent2;
