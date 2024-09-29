import { FunctionComponent, memo, useMemo, type CSSProperties } from "react";
import {ProfileStatisticsContainerType} from "../types/ProfileStatisticsContainerType";

const ProfileStatisticsContainer: FunctionComponent<ProfileStatisticsContainerType> =
  memo(
    ({ className = "", propFlex, propAlignSelf, propHeight, propLeft, bG }) => {
      const profileStatisticsContainerStyle: CSSProperties = useMemo(() => {
        return {
          flex: propFlex,
          alignSelf: propAlignSelf,
        };
      }, [propFlex, propAlignSelf]);

      const resourceOverviewStyle: CSSProperties = useMemo(() => {
        return {
          height: propHeight,
          left: propLeft,
        };
      }, [propHeight, propLeft]);

      return (
        <div
          className={`flex-1 flex flex-row items-start justify-start relative max-w-full text-center text-[1rem] text-[#3c3c3c] font-[Inter] ${className}`}
          style={profileStatisticsContainerStyle}
        >
          <div
            className="h-[105.625rem] w-[63.5rem] absolute !m-[0] top-[-56.687rem] left-[-2.875rem]"
            style={resourceOverviewStyle}
          >
            <img
              className="absolute top-[0rem] left-[0rem] rounded-[40px] w-full h-full"
              alt=""
              src={bG}
            />
            <img
              className="absolute top-[32.563rem] left-[53.938rem] w-[0.938rem] h-[0.938rem] overflow-hidden z-[1]"
              alt=""
              src="/src/assets/-2-2.svg"
            />
          </div>
          <div className="flex-1 rounded-[20px] bg-[#f0f4f9] flex flex-row items-start justify-start pt-[2.75rem] px-[4.625rem] pb-[2.812rem] box-border gap-[7.437rem] max-w-full z-[3] mq450:gap-[1.875rem] mq1050:gap-[3.688rem] mq1050:flex-wrap mq1050:pl-[2.313rem] mq1050:pr-[2.313rem] mq1050:box-border">
            <div className="h-[15.563rem] w-[57.938rem] relative rounded-[20px] bg-[#f0f4f9] hidden max-w-full" />
            <div className="flex flex-col items-start justify-start gap-[1.687rem] min-w-[16rem] mq1050:flex-1">
              <b className="w-[3.125rem] relative inline-block z-[1]">Огляд</b>
              <div className="self-stretch flex flex-col items-start justify-start gap-[0.875rem] text-left text-[0.875rem]">
                <div className="self-stretch h-[1.125rem] flex flex-row items-start justify-start flex-wrap content-start relative gap-x-[5.875rem] gap-y-[0.062rem] z-[1]">
                  <div className="absolute !m-[0] top-[0rem] left-[0rem] inline-block min-w-[6.938rem]">
                    Охват аккаунтів
                  </div>
                  <div className="absolute !m-[0] top-[0rem] left-[13.125rem] inline-block min-w-[2.938rem]">
                    12 090
                  </div>
                  <div className="h-[0.031rem] w-[15.969rem] absolute !m-[0] top-[1.125rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border" />
                </div>
                <div className="self-stretch h-[1.125rem] flex flex-row items-start justify-start flex-wrap content-start relative gap-x-[4.312rem] gap-y-[0.062rem] z-[1]">
                  <div className="absolute !m-[0] top-[0rem] left-[0rem]">
                    Взаємодії з контентом
                  </div>
                  <div className="absolute !m-[0] top-[0rem] left-[14.25rem] inline-block min-w-[1.75rem]">
                    930
                  </div>
                  <div className="h-[0.031rem] w-[15.969rem] absolute !m-[0] top-[1.125rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border" />
                </div>
                <div className="self-stretch h-[1.125rem] flex flex-row items-start justify-start flex-wrap content-start relative gap-x-[8.75rem] gap-y-[0.062rem] z-[1]">
                  <div className="absolute !m-[0] top-[0rem] left-[0rem] inline-block min-w-[5.313rem]">
                    Дії в профілі
                  </div>
                  <div className="absolute !m-[0] top-[0rem] left-[14.313rem] inline-block min-w-[1.625rem]">
                    100
                  </div>
                  <div className="h-[0.031rem] w-[15.906rem] absolute !m-[0] top-[1.125rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border" />
                </div>
              </div>
            </div>
            <div className="flex flex-col items-start justify-start gap-[1.625rem] min-w-[17.75rem] mq1050:flex-1">
              <div className="w-[16.563rem] flex flex-row items-start justify-between gap-[1.25rem]">
                <b className="relative inline-block min-w-[6.375rem] z-[1]">
                  Дії в профілі
                </b>
                <b className="relative z-[1]">13 100</b>
              </div>
              <div className="self-stretch flex flex-col items-start justify-start gap-[0.875rem] text-left text-[0.875rem]">
                <div className="self-stretch h-[1.125rem] flex flex-row items-start justify-start flex-wrap content-start relative gap-x-[3.937rem] gap-y-[0.062rem] z-[1]">
                  <div className="absolute !m-[0] top-[0rem] left-[0rem]">
                    Відвідування профілю
                  </div>
                  <div className="absolute !m-[0] top-[0rem] left-[14rem] inline-block min-w-[2.5rem]">
                    8 389
                  </div>
                  <div className="h-[0.031rem] w-[16.406rem] absolute !m-[0] top-[1.125rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border" />
                </div>
                <div className="self-stretch h-[1.125rem] flex flex-row items-start justify-start flex-wrap content-start relative gap-x-[9.687rem] gap-y-[0.062rem] z-[1]">
                  <div className="absolute !m-[0] top-[0rem] left-[0rem] inline-block min-w-[4rem]">
                    Підписки
                  </div>
                  <div className="absolute !m-[0] top-[0rem] left-[14rem] inline-block min-w-[2.563rem]">
                    3 360
                  </div>
                  <div className="h-[0.031rem] w-[16.406rem] absolute !m-[0] top-[1.125rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border" />
                </div>
                <div className="self-stretch h-[1.125rem] flex flex-row items-end justify-start flex-wrap content-end relative gap-x-[1.5rem] gap-y-[0.062rem] z-[1]">
                  <div className="absolute !m-[0] top-[0rem] left-[0rem]">
                    Переходи на слідуючу історію
                  </div>
                  <div className="absolute !m-[0] top-[0rem] left-[14.875rem] inline-block min-w-[1.625rem]">
                    314
                  </div>
                  <div className="h-[0.031rem] w-[16.406rem] absolute !m-[0] top-[1.125rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border" />
                </div>
                <div className="self-stretch h-[1.125rem] flex flex-row items-end justify-start flex-wrap content-end relative gap-x-[12.625rem] gap-y-[0.062rem] z-[1]">
                  <div className="absolute !m-[0] top-[0rem] left-[0rem] inline-block min-w-[2.438rem]">
                    Вихід
                  </div>
                  <div className="absolute !m-[0] top-[0rem] left-[15.375rem] inline-block min-w-[1.188rem]">
                    98
                  </div>
                  <div className="h-[0.031rem] w-[16.406rem] absolute !m-[0] top-[1.125rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border" />
                </div>
              </div>
            </div>
          </div>
        </div>
      );
    }
  );

export default ProfileStatisticsContainer;
