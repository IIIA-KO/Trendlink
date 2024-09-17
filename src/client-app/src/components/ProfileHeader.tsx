import { FunctionComponent, useMemo, type CSSProperties } from "react";
import EditButton from "./EditButton";
import MenuSwitcher from "../components/PageSwitcher";
import "./styleMenu.css";
import newLocal from "../public/mynauitelephone.svg";
import newLocal_1 from "../public/mageemail.svg";
import newLocal_2 from "../public/tiktok-icon.svg";
import newLocal_3 from "../public/instagram-icon.svg";
import newLocal_4 from "/facebook-icon.svg";
import newLocal_5 from "/-3-2.svg";

export type ProfileHeaderType = {
  className?: string;
  profilePicture?: string;
  group95?: string;
  editButtonBorderRadius?: string;
  editButtonBackgroundColor?: string;
  editButtonOverflow?: string;
  editButtonDisplay?: string;
  editButtonFlexDirection?: string;
  editButtonPadding?: string;

  /** Style props */
  groupDivLeft?: CSSProperties["left"];
  groupDivTop?: CSSProperties["top"];
  groupDivRight?: CSSProperties["right"];
  divTextDecoration?: CSSProperties["textDecoration"];
};

const ProfileHeader: FunctionComponent<ProfileHeaderType> = ({
  className = "",
  groupDivLeft,
  groupDivTop,
  groupDivRight,
  profilePicture,
  group95,
  divTextDecoration,
  editButtonBorderRadius,
  editButtonBackgroundColor,
  editButtonOverflow,
  editButtonDisplay,
  editButtonFlexDirection,
  editButtonPadding,
}) => {
  const profileHeaderStyle: CSSProperties = useMemo(() => {
    return {
      left: groupDivLeft,
      top: groupDivTop,
      right: groupDivRight,
    };
  }, [groupDivLeft, groupDivTop, groupDivRight]);

  const div3Style: CSSProperties = useMemo(() => {
    return {
      textDecoration: divTextDecoration,
    };
  }, [divTextDecoration]);


  return (
    <div
      className={`w-[926px] !m-[0] absolute top-[35px] left-[-10px] flex flex-row items-start justify-between max-w-full gap-5 z-[2] text-left text-base text-main-black font-inter mq1050:flex-wrap ${className}`}
      style={profileHeaderStyle}
    >
      <div className="w-[553px] flex flex-col items-start justify-start gap-[25px] max-w-full">
        <div className="self-stretch flex flex-col items-start justify-start gap-[30px] max-w-full">
          <div className="w-[519px] flex flex-row items-start justify-start gap-[30px] max-w-full mq750:flex-wrap">
            <img
              className="h-[110px] w-[110px] relative rounded-[50%] object-cover min-h-[110px]"
              loading="lazy"
              alt=""
              src={profilePicture}
            />
            <div className="flex-1 flex flex-row items-start justify-start gap-11 min-w-[246px] max-w-full mq450:flex-wrap">
              <div className="flex-1 flex flex-col items-start justify-start gap-2.5 min-w-[131px]">
                <div className="self-stretch flex flex-col items-start justify-start gap-[9px]">
                  <a className="[text-decoration:none] relative font-bold text-[inherit]">
                    Давиденко Наталія
                  </a>
                  <div className="self-stretch h-[19px] flex flex-row items-start justify-start pt-0 px-0 pb-1 box-border gap-2 text-gray-200">
                    <div className="flex flex-col items-start justify-start pt-[3px] px-0 pb-0">
                      <div className="w-[15px] h-3 relative shrink-0">
                        <div className="absolute h-full w-full top-[0%] right-[0%] bottom-[0%] left-[0%] rounded bg-crimson" />
                        <img
                          className="absolute h-[41.67%] w-[33.33%] top-[25%] right-[33.33%] bottom-[33.33%] left-[33.33%] max-w-full overflow-hidden max-h-full object-contain z-[1]"
                          alt=""
                          src="/badge-shape.svg"
                        />
                      </div>
                    </div>
                    <blockquote className="m-0 relative shrink-0">
                      “Стиль на кожен день”
                    </blockquote>
                  </div>
                </div>
                <div className="flex flex-row items-start justify-start pt-0 px-0 pb-[5px] text-sm">
                  <a className="[text-decoration:none] relative text-[inherit] inline-block min-w-[89px]">
                    Мода і стиль
                  </a>
                </div>
                <img
                  className="w-[100px] h-5 relative"
                  loading="lazy"
                  alt=""
                  src={group95}
                />
              </div>
              <div className="flex flex-col items-start justify-start pt-px px-0 pb-0 text-sm">
                <div className="flex flex-col items-start justify-start gap-3">
                  <div className="relative" style={div3Style}>
                    Особистий аккаунт
                  </div>
                  <div className="flex flex-row items-start justify-start gap-0.5 text-text">
                    <div className="flex flex-col items-start justify-start pt-px px-0 pb-0">
                      <img
                        className="w-[15px] h-[15px] relative object-cover shrink-0"
                        alt=""
                        src="/planning--travel--location--24@2x.png"
                      />
                    </div>
                    <a className="[text-decoration:none] relative text-[inherit] inline-block min-w-[54px] shrink-0">
                      UA/Kyiv
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div className="self-stretch flex flex-row items-start justify-start py-0 pl-1 pr-0 box-border max-w-full text-sm">
            <div className="flex-1 relative inline-block max-w-full">
              Ласкаво просимо у світ стилю та моди! 💄👗✨ Я - Наталія,
              фешн-ентузіаст і пристрасний любитель трендів. Тут ви знайдете
              натхнення для створення неповторних образів, поради щодо вибору
              актуальних нарядів та аксесуарів, а також огляди на улюблені
              Підписуйтесь, щоб залишатися в курсі останніх модних тенденцій і
              ділитися своїми враженнями зі мною!.
            </div>
          </div>
        </div>
        <div className="flex flex-row items-start  py-0 px-2 text-sm text-white">
          <EditButton
            editButtonBorderRadius={editButtonBorderRadius}
            editButtonBackgroundColor={editButtonBackgroundColor}
            editButtonOverflow={editButtonOverflow}
            editButtonDisplay={editButtonDisplay}
            editButtonPadding={editButtonPadding}
          />
          <div className="pl-[2rem]"><MenuSwitcher></MenuSwitcher></div>
          
        </div>
      </div>
      <div className="flex flex-col items-start justify-start pt-[75px] px-0 pb-0 text-xs">
        <div className="flex flex-col items-end justify-start gap-2.5">
          <div className="flex flex-col items-start justify-start gap-[5px]">
            <div className="flex flex-row items-start justify-start gap-0.5">
              <div className="flex flex-col items-start justify-start pt-[3px] px-0 pb-0">
                <img
                  className="w-2.5 h-2.5 relative overflow-hidden shrink-0"
                  alt=""
                  src={newLocal}
                />
              </div>
              <div className="relative inline-block min-w-[116px]">
                +38(067) 695 66 52
              </div>
            </div>
            <div className="flex flex-row items-start justify-start py-0 pl-px pr-0 text-center">
              <div className="flex flex-row items-start justify-start pt-0 px-0 pb-px gap-0.5">
                <div className="flex flex-col items-start justify-start pt-1 px-0 pb-0">
                  <img
                    className="w-[11px] h-[9px] relative overflow-hidden shrink-0"
                    alt=""
                    src={newLocal_1}
                  />
                </div>
                <div className="relative inline-block min-w-[122px] shrink-0 whitespace-nowrap">
                  nataNata@gmail.com
                </div>
              </div>
            </div>
          </div>
          <div className="flex flex-row items-start justify-end py-0 px-[7px]">
            <div className="flex flex-row items-start justify-start py-0.5 px-0 gap-2">
              <img
                className="h-5 w-5 relative overflow-hidden shrink-0 min-h-[20px]"
                loading="lazy"
                alt=""
                src={newLocal_2}
              />
              <img
                className="h-5 w-5 relative overflow-hidden shrink-0 min-h-[20px]"
                loading="lazy"
                alt=""
                src={newLocal_3}
              />
              <img
                className="h-5 w-5 relative overflow-hidden shrink-0 min-h-[20px]"
                loading="lazy"
                alt=""
                src={newLocal_4}
              />
              <img
                className="h-5 w-5 relative overflow-hidden shrink-0 min-h-[20px]"
                alt=""
                src={newLocal_5}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfileHeader;
