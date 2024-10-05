import { FunctionComponent } from "react";
import "antd/dist/reset.css";
import { Input, DatePicker, Select } from "antd";
import FrameComponent from "./FrameComponent";
import Days from "./Days";
import CollaborationActions from "./CollaborationActions";

export type GroupComponent4Type = {
  className?: string;
};

const GroupComponent4: FunctionComponent<GroupComponent4Type> = ({
  className = "",
}) => {
  return (
    <div
      className={`flex-1 shadow-[3px_3px_10px_2px_rgba(108,_108,_108,_0.2)] rounded-xl bg-whitesmoke-100 flex flex-col items-end justify-start pt-5 px-0 pb-0 box-border gap-[63px] max-w-full z-[5] text-left text-sm text-text-2 font-inter ${className}`}
    >
      <div className="relative text-black text-center hidden">
        найближча інтеграція 1.03.24
      </div>
      <div className="self-stretch h-[550px] relative shadow-[3px_3px_10px_2px_rgba(108,_108,_108,_0.2)] rounded-xl bg-whitesmoke-100 hidden" />
      <div className="w-[842px] flex flex-row items-start justify-end py-0 px-5 box-border max-w-full text-main-black">
        <div className="flex-1 flex flex-col items-end justify-start gap-2.5 max-w-full">
          <img
            className="w-[25px] h-[25px] relative object-cover z-[1]"
            loading="lazy"
            alt=""
            src="/actions@2x.png"
          />
          <div className="self-stretch flex flex-row items-start justify-end py-0 pl-0 pr-[27px] box-border max-w-full">
            <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-5 mq1050:flex-wrap">
              <div className="w-[333px] flex flex-col items-start justify-start gap-[86px] min-w-[333px] max-w-full mq1050:flex-1">
                <div className="self-stretch flex flex-col items-start justify-start gap-[15px]">
                  <Input
                    className="bg-[transparent] w-[299px] font-inter font-bold text-sm text-main-black"
                    style={{ width: "299px" }}
                    bordered={true}
                  />
                  <Input
                    className="bg-[transparent] self-stretch font-inter text-sm text-main-black"
                    bordered={true}
                  />
                </div>
                <FrameComponent />
              </div>
              <div className="h-[323px] w-[328px] flex flex-col items-start justify-start gap-2.5 min-w-[328px] max-w-full mq1050:flex-1">
                <DatePicker
                  className="w-[249px]"
                  placeholder="Рік"
                  allowClear={false}
                  bordered={false}
                />
                <div className="self-stretch flex-1 flex flex-col items-end justify-start">
                  <div className="self-stretch flex flex-row items-start justify-start">
                    <div className="w-[266px] flex flex-row items-start justify-start relative">
                      <Input.TextArea
                        className="h-[109px] w-[329px] absolute !m-[0] right-[-65px] bottom-[-14px] z-[1]"
                        style={{ width: "329px" }}
                      />
                      <div className="w-[118px] absolute !m-[0] top-[83px] right-[-60px] inline-block z-[1]">
                        Дата неможлива для співпраці
                      </div>
                      <div className="flex-1 shadow-[2px_2px_10px_rgba(150,_150,_150,_0.11)] rounded-xl bg-white overflow-hidden flex flex-col items-end justify-start pt-[22px] pb-[27px] pl-[7px] pr-2 gap-[19.2px] z-[6] text-center text-darkslategray-100">
                        <div className="self-stretch flex flex-col items-end justify-start gap-3">
                          <div className="self-stretch flex flex-row items-start justify-between py-0 px-[50px] gap-5">
                            <img
                              className="h-4 w-4 relative overflow-hidden shrink-0"
                              alt=""
                              src="/icons-7.svg"
                            />
                            <div className="relative tracking-[0.01em] uppercase inline-block min-w-[73px]">
                              Березень
                            </div>
                            <img
                              className="h-4 w-4 relative overflow-hidden shrink-0 object-contain"
                              alt=""
                              src="/icons-8@2x.png"
                            />
                          </div>
                          <div className="self-stretch h-[0.8px] relative border-neutral-blue-black-40 border-t-[0.8px] border-solid box-border" />
                        </div>
                        <div className="self-stretch flex flex-row items-start justify-end py-0 pl-[18px] pr-[17px] text-3xs text-slategray">
                          <div className="flex-1 flex flex-col items-start justify-start gap-[9px]">
                            <div className="self-stretch flex flex-row items-start justify-start py-0 px-6">
                              <div className="flex-1 flex flex-row items-start justify-start">
                                <div className="flex-1 relative uppercase">
                                  пн
                                </div>
                                <div className="flex-1 relative uppercase">
                                  вт
                                </div>
                                <div className="flex-1 relative uppercase">
                                  ср
                                </div>
                                <div className="flex-1 relative uppercase">
                                  чт
                                </div>
                                <div className="flex-1 relative uppercase">
                                  пт
                                </div>
                                <div className="flex-1 relative uppercase">
                                  сб
                                </div>
                                <div className="flex-1 relative uppercase">
                                  нд
                                </div>
                              </div>
                            </div>
                            <div className="self-stretch flex flex-col items-start justify-start gap-[5px]">
                              <div className="self-stretch flex flex-row items-start justify-start py-0 px-6">
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="1"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="2"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="3"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="4"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="5"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="6"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="7"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                              </div>
                              <div className="self-stretch flex flex-row items-start justify-start py-0 px-6">
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="8"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="40px"
                                  propBackgroundColor="#009ea0"
                                  propOverflow="hidden"
                                  prop="9"
                                  propColor="#fdfdfd"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="10"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="11"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="12"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="13"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="14"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                              </div>
                              <div className="self-stretch flex flex-row items-start justify-start py-0 px-6">
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="15"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="16"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="17"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="18"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="19"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="20"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="21"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                              </div>
                              <div className="self-stretch flex flex-row items-start justify-start py-0 px-6">
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="22"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="23"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="24"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="25"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="26"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="27"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="28"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                              </div>
                              <div className="self-stretch flex flex-row items-start justify-start py-0 px-6 gap-[4.8px]">
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="29"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="30"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="31"
                                  propColor="#3c3c3c"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="1"
                                  propColor="rgba(60, 60, 60, 0)"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="1"
                                  propColor="rgba(60, 60, 60, 0)"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                                <Days
                                  propWidth="unset"
                                  propFlex="1"
                                  propBorderRadius="unset"
                                  propBackgroundColor="unset"
                                  propOverflow="unset"
                                  prop="1"
                                  propColor="rgba(60, 60, 60, 0)"
                                  propTextShadow="unset"
                                  propTextDecoration="unset"
                                />
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                      <img
                        className="h-3.5 w-[19px] absolute !m-[0] top-[-9px] left-[85px] rounded-12xs z-[7]"
                        loading="lazy"
                        alt=""
                        src="/polygon-2.svg"
                      />
                    </div>
                  </div>
                  <Select
                    className="w-[177px] font-inter text-sm text-main-black z-[1] mt-[-233px]"
                    style={{ width: "177px" }}
                    virtual={false}
                    showArrow={false}
                  >{` `}</Select>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <i className="relative text-xs hidden font-light min-w-[45px]">Години:</i>
      <div className="w-[57px] h-[35px] relative hidden text-transparent">
        <div className="absolute top-[0px] left-[0px] rounded-8xs bg-white w-full h-full" />
        <div className="absolute top-[10px] left-[8px] font-light inline-block min-w-[17px]">
          hh
        </div>
      </div>
      <i className="relative text-xs hidden font-light min-w-[53px]">
        Хвилини:
      </i>
      <div className="w-[57px] h-[35px] relative hidden text-transparent">
        <div className="absolute top-[0px] left-[0px] rounded-8xs bg-white w-full h-full" />
        <div className="absolute top-[10px] left-[8px] font-light inline-block min-w-[25px]">
          mm
        </div>
      </div>
      <i className="relative text-xs hidden font-light min-w-[41px]">Статус</i>
      <i className="relative text-xs hidden font-light min-w-[116px]">
        Коментарі і нотатки
      </i>
      <div className="w-[47px] h-6 relative hidden">
        <div className="absolute h-full w-full top-[0%] right-[0%] bottom-[0%] left-[0%] rounded-mini bg-whitesmoke-200" />
        <div className="absolute h-full w-[48.94%] top-[0%] right-[0%] bottom-[0%] left-[51.06%] rounded-mini bg-main-green" />
      </div>
      <div className="relative text-main-black hidden min-w-[65px]">
        Giveaway
      </div>
      <div className="w-[47px] h-6 relative hidden">
        <div className="absolute h-full w-full top-[0%] right-[0%] bottom-[0%] left-[0%] rounded-mini bg-whitesmoke-200" />
        <div className="absolute h-full w-[48.94%] top-[0%] right-[51.06%] bottom-[0%] left-[0%] rounded-mini bg-darkgray" />
      </div>
      <CollaborationActions />
    </div>
  );
};

export default GroupComponent4;
