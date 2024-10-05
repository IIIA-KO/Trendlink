import { FunctionComponent, memo, useMemo, type CSSProperties } from "react";
import EditButton from "./Buttons/EditButton";
import {FrameComponent3Type} from "../types/FrameComponent3Type";

const FrameComponent3: FunctionComponent<FrameComponent3Type> = memo(
  ({
    className = "",
    profilePicture,
    group95,
    propTextDecoration,
    prop,
    editButtonBorderRadius,
    editButtonBackgroundColor,
    editButtonOverflow,
    editButtonDisplay,
    editButtonFlexDirection,
    editButtonPadding,
    editButtonPosition,
    editButtonTop,
    editButtonLeft,
    divDisplay,
    divMinWidth,
  }) => {
    const div18Style: CSSProperties = useMemo(() => {
      return {
        textDecoration: propTextDecoration,
      };
    }, [propTextDecoration]);

    return (
      <div
        className={`w-[57.938rem] h-[17.938rem] absolute  top-[4.375rem] right-[9.063rem] text-left text-[1rem] text-[#3c3c3c] font-[Inter] mq1050:h-auto mq1050:min-h-[287] ${className}`}
      >
        <div className="absolute top-[0rem] left-[0rem] fixed flex flex-row items-start justify-between pt-[0rem] px-[0rem] pb-[3.875rem] box-border max-w-full gap-[1.25rem] h-full z-[1] mq1050:flex-wrap">
          <div className="w-[34.563rem] flex flex-col items-start justify-start gap-[1.875rem] max-w-full">
            <div className="flex flex-row items-start justify-start gap-[1.875rem] max-w-full mq750:flex-wrap">
              <img
                className="h-[6.875rem] w-[6.875rem] relative rounded-[50%] object-cover"
                loading="lazy"
                alt=""
                src={profilePicture}
              />
              <div className="flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[3.562rem] gap-[0.937rem]">
                <div className="flex flex-col items-start justify-start gap-[0.625rem]">
                  <a className="[text-decoration:none] relative font-bold text-[inherit]">
                    Морозюк Вікторія
                  </a>
                  <div className="h-[1.188rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.25rem] box-border gap-[0.5rem] text-[#181d25]">
                    <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                      <div className="w-[0.938rem] h-[0.75rem] relative shrink-0">
                        <div className="absolute h-full w-full top-[0%] right-[0%] bottom-[0%] left-[0%] rounded-[4px] bg-[#dd1e1e]" />
                        <img
                          className="absolute h-[41.67%] w-[33.33%] top-[25%] right-[33.33%] bottom-[33.33%] left-[33.33%] max-w-full overflow-hidden max-h-full object-contain z-[1]"
                          alt=""
                          src="/src/assets/badge-shape.svg"
                        />
                      </div>
                    </div>
                    <a className="[text-decoration:none] relative text-[inherit] shrink-0">
                      “Образ на міліон”
                    </a>
                  </div>
                  <a className="[text-decoration:none] relative text-[0.875rem] text-[inherit] inline-block min-w-[5.625rem]">
                    Мода і стиль
                  </a>
                </div>
                <img
                  className="w-[6.25rem] h-[1.25rem] relative"
                  loading="lazy"
                  alt=""
                  src={group95}
                />
              </div>
              <div className="flex flex-col items-start justify-start pt-[0.062rem] px-[0rem] pb-[0rem] text-[0.875rem]">
                <div className="flex flex-col items-start justify-start gap-[0.75rem]">
                  <div className="relative" style={div18Style}>
                    Особистий аккаунт
                  </div>
                  <div className="flex flex-row items-start justify-start gap-[0.125rem] text-[#444]">
                    <div className="flex flex-col items-start justify-start pt-[0.062rem] px-[0rem] pb-[0rem]">
                      <img
                        className="w-[0.938rem] h-[0.938rem] relative object-cover"
                        loading="lazy"
                        alt=""
                        src="/src/assets/planning--travel--location--24@2x.png"
                      />
                    </div>
                    <a className="[text-decoration:none] relative text-[inherit] inline-block min-w-[3.938rem]">
                      UA/Rivne
                    </a>
                  </div>
                </div>
              </div>
            </div>
            <div className="self-stretch flex flex-row items-start justify-start py-[0rem] pl-[0.25rem] pr-[0rem] box-border max-w-full text-[0.875rem]">
              <div className="flex-1 relative inline-block max-w-full">
                Ласкаво прошу до моєї сторінки! Я — експерт у світі моди, готова
                поділитися з вами останніми тенденціями, стильними образами та
                ідеями, що надихають. Тут ви знайдете ексклюзивні поради щодо
                стилю, огляди модних колекцій та рекомендації щодо створення
                унікального гардеробу. Приєднуйтесь, щоб бути в курсі всіх
                модних новинок.
              </div>
            </div>
          </div>
          <div className="flex flex-col items-start justify-start pt-[4.687rem] px-[0rem] pb-[0rem] text-[0.75rem]">
            <div className="flex flex-col items-end justify-start gap-[0.625rem]">
              <div className="flex flex-col items-start justify-start gap-[0.312rem]">
                <div className="flex flex-row items-start justify-start gap-[0.125rem]">
                  <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                    <img
                      className="w-[0.625rem] h-[0.625rem] relative overflow-hidden shrink-0"
                      alt=""
                      src="/src/assets/mynauitelephone.svg"
                    />
                  </div>
                  <div className="relative inline-block min-w-[7.313rem]">
                    +38(067) 695 66 52
                  </div>
                </div>
                <div className="flex flex-row items-start justify-start py-[0rem] pl-[0.062rem] pr-[0rem] text-center">
                  <div className="w-[8.375rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.062rem] box-border gap-[0.125rem]">
                    <div className="flex flex-col items-start justify-start pt-[0.25rem] px-[0rem] pb-[0rem]">
                      <img
                        className="w-[0.688rem] h-[0.563rem] relative overflow-hidden shrink-0"
                        alt=""
                        src="/src/assets/mageemail.svg"
                      />
                    </div>
                    <div className="flex-1 relative inline-block min-w-[7.688rem] shrink-0">
                      nataNata@gmail.com
                    </div>
                  </div>
                </div>
              </div>
              <div className="flex flex-row items-start justify-end py-[0rem] px-[0.437rem]">
                <div className="flex flex-row items-start justify-start py-[0.125rem] px-[0rem] gap-[0.5rem]">
                  <img
                    className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
                    loading="lazy"
                    alt=""
                    src="/src/assets/icons/tiktok-icon.svg"
                  />
                  <img
                    className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
                    loading="lazy"
                    alt=""
                    src="/src/assets/icons/instagram-icon.svg"
                  />
                  <img
                    className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
                    loading="lazy"
                    alt=""
                    src="/src/assets/icons/facebook-icon.svg"
                  />
                  <img
                    className="h-[1.25rem] w-[1.25rem] relative overflow-hidden shrink-0"
                    loading="lazy"
                    alt=""
                    src="/src/assets/-3-2.svg"
                  />
                </div>
              </div>
            </div>
          </div>
          <div>
          <div className="w-[15.625rem] top-[rem] rounded-[40px] bg-[#009ea0] overflow-hidden shrink-0 hidden flex-row items-center justify-center py-[0.625rem] px-[1.562rem] box-border whitespace-nowrap text-[0.875rem] text-[#fff]">
            <div className="relative">Редагувати профіль</div>
          </div>
        </div>
        <EditButton
          editButtonBorderRadius={editButtonBorderRadius}
          editButtonBackgroundColor={editButtonBackgroundColor}
          editButtonOverflow={editButtonOverflow}
          editButtonDisplay={editButtonDisplay}
          editButtonPadding={editButtonPadding}
        />
        </div>
      </div>
    );
  }
);

export default FrameComponent3;
