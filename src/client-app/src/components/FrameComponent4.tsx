import { FunctionComponent, memo } from "react";
import Component5 from "./Component5 (2)";
import Component7 from "./Component7";
import ProfileStatisticsContainer from "./ProfileStatisticsContainer";
import {ClassNameType} from "../types/ClassNameType";

const FrameComponent4: FunctionComponent<ClassNameType> = memo(
  ({ className = "" }) => {
    return (
      <div
        className={`self-stretch flex flex-col items-start justify-start gap-[1.875rem] max-w-full text-left text-[0.75rem] text-[#7c7c7c] font-[Inter] ${className}`}
      >
        <div className="self-stretch flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[1.562rem] gap-[0.25rem] mq1050:flex-wrap">
          <Component5
            propLeft="unset"
            propBorder="1px solid #c0bebe"
            propBackgroundColor="unset"
            prop="Аудиторія"
            propColor="#3c3c3c"
            propMinWidth="4.313rem"
            component7Top="unset"
            component7Padding="0.562rem 3.375rem 0.562rem 3.562rem"
            component7Height="2.438rem"
            component7Position="unset"
            divDisplay="inline-block"
          />
          <Component5
            propLeft="unset"
            propBorder="1px solid #c0bebe"
            propBackgroundColor="unset"
            prop="Публікації"
            propColor="#3c3c3c"
            propMinWidth="4.438rem"
            component7Top="unset"
            component7Padding="0.562rem 3.312rem 0.562rem 3.5rem"
            component7Height="2.438rem"
            component7Position="unset"
            divDisplay="inline-block"
          />
          <Component5
            propLeft="unset"
            propBorder="1px solid #c0bebe"
            propBackgroundColor="unset"
            prop="Історії"
            propColor="#3c3c3c"
            propMinWidth="unset"
            component7Top="unset"
            component7Padding="0.562rem 4.25rem"
            component7Height="2.438rem"
            component7Position="unset"
            divDisplay="unset"
          />
          <Component5
            propLeft="unset"
            propBorder="unset"
            propBackgroundColor="#009ea0"
            prop="Reels"
            propColor="#fff"
            propMinWidth="unset"
            component7Top="unset"
            component7Padding="0.687rem 4.5rem 0.687rem 4.562rem"
            component7Height="unset"
            component7Position="unset"
            divDisplay="unset"
          />
          <Component5
            propLeft="unset"
            propBorder="1px solid #c0bebe"
            propBackgroundColor="unset"
            prop="Залучення"
            propColor="#3c3c3c"
            propMinWidth="unset"
            component7Top="unset"
            component7Padding="0.562rem 3.312rem"
            component7Height="2.438rem"
            component7Position="unset"
            divDisplay="unset"
          />
        </div>
        <div className="self-stretch flex flex-col items-start justify-start gap-[1.312rem] max-w-full">
          <div className="self-stretch flex flex-row items-start justify-end">
            <div className="relative [text-decoration:underline] z-[1]">
              Зберегти в PDF
            </div>
          </div>
          <div className="self-stretch flex flex-row items-start justify-start gap-[1.875rem] max-w-full text-[1rem] text-[#fff] mq1050:flex-wrap">
            <div className="flex flex-row items-start justify-start relative max-w-full mq750:min-w-full mq1050:flex-1">
              <div className="w-[21.5rem] !m-[0] absolute top-[-24.937rem] left-[-12.125rem] flex flex-row items-start justify-start max-w-full">
                <div className="flex-1 flex flex-col items-start justify-start max-w-full z-[2]">
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.093rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[2.188rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/.svg"
                    />
                    <div className="relative uppercase hidden">Профіль</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.093rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[2.188rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-1.svg"
                    />
                    <div className="relative uppercase hidden">Пошук</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.093rem] px-[2.625rem] pb-[1.1rem] gap-[1.875rem]">
                    <img
                      className="h-[2.181rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-2.svg"
                    />
                    <div className="relative uppercase hidden">Чат</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.168rem] px-[2.625rem] pb-[1.175rem] gap-[1.875rem]">
                    <img
                      className="h-[2.031rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-3.svg"
                    />
                    <div className="relative uppercase hidden">статистика</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.168rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[2.038rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-4.svg"
                    />
                    <div className="relative uppercase hidden">календар</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.112rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[2.15rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-31.svg"
                    />
                    <div className="relative uppercase hidden">відгуки</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.293rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[1.788rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-5.svg"
                    />
                    <div className="relative uppercase hidden">новини</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.268rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[1.838rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-6.svg"
                    />
                    <div className="relative uppercase hidden">Сповіщення</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[0.9rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[2.575rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-7.svg"
                    />
                    <div className="relative uppercase hidden">Збережені</div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.093rem] px-[2.625rem] gap-[1.875rem]">
                    <img
                      className="h-[2.188rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-8.svg"
                    />
                    <div className="relative uppercase hidden">
                      Умови співпраці
                    </div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.193rem] px-[2.625rem] pb-[1.2rem] gap-[1.875rem]">
                    <img
                      className="h-[1.981rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/-9.svg"
                    />
                    <div className="relative uppercase hidden">
                      Налаштування
                    </div>
                  </div>
                  <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.112rem] px-[2.625rem] pb-[1.118rem] gap-[1.875rem] z-[1]">
                    <img
                      className="h-[2.144rem] w-[2.188rem] relative overflow-hidden shrink-0"
                      loading="lazy"
                      alt=""
                      src="/src/assets/--3.svg"
                    />
                    <div className="relative uppercase hidden">
                      служба підтримки
                    </div>
                  </div>
                </div>
                <img
                  className="h-[0.938rem] w-[0.938rem] absolute !m-[0] top-[22.75rem] right-[8.438rem] overflow-hidden shrink-0 z-[3]"
                  loading="lazy"
                  alt=""
                  src="/src/assets/fluentemojiflatspiralcalendar.svg"
                />
                <Component7 />
              </div>
              <div className="flex-1 rounded-[20px] bg-[#f0f4f9] flex flex-row items-start justify-start pt-[1.437rem] px-[2.812rem] pb-[1.687rem] box-border gap-[0.625rem] max-w-full z-[4] text-[0.875rem] text-[#3c3c3c] mq450:flex-wrap mq450:pl-[1.25rem] mq450:pr-[1.25rem] mq450:box-border">
                <div className="h-[20rem] w-[23.375rem] relative rounded-[20px] bg-[#f0f4f9] hidden max-w-full" />
                <div className="flex flex-col items-start justify-start pt-[7.5rem] px-[0rem] pb-[0rem]">
                  <img
                    className="w-[1.25rem] h-[1.25rem] relative object-contain z-[1]"
                    loading="lazy"
                    alt=""
                    src="/src/assets/actions--navigation--chevronright--25.svg"
                  />
                </div>
                <div className="flex-1 rounded-[10px] border-[#d9d9d9] border-[1px] border-solid box-border flex flex-col items-start justify-start pt-[1.187rem] px-[0rem] pb-[1.062rem] gap-[0.937rem] min-w-[9.125rem] z-[2]">
                  <div className="self-stretch flex flex-row items-start justify-start py-[0rem] pl-[3rem] pr-[3.062rem]">
                    <div className="flex flex-row items-start justify-start gap-[0.25rem]">
                      <div className="flex flex-col items-start justify-start pt-[0.062rem] px-[0rem] pb-[0rem]">
                        <img
                          className="w-[0.938rem] h-[0.938rem] relative overflow-hidden shrink-0 z-[1]"
                          loading="lazy"
                          alt=""
                          src="/src/assets/skilliconsinstagram2.svg"
                        />
                      </div>
                      <div className="relative inline-block min-w-[6.688rem] z-[1]">
                        1,8% залучення
                      </div>
                    </div>
                  </div>
                  <div className="self-stretch h-[9.813rem] relative">
                    <img
                      className="absolute top-[0rem] left-[0rem] w-full h-full object-cover z-[1]"
                      alt=""
                      src="/src/assets/group-1172@2x.png"
                    />
                    <img
                      className="absolute top-[4.188rem] left-[6.188rem] rounded-[100px] w-[1.5rem] h-[1.5rem] z-[3]"
                      loading="lazy"
                      alt=""
                      src="/src/assets/actions--navigation--caretright--24.svg"
                    />
                  </div>
                  <div className="self-stretch h-[16.875rem] relative rounded-[10px] border-[#d9d9d9] border-[1px] border-solid box-border hidden" />
                  <div className="self-stretch flex flex-row items-start justify-start py-[0rem] pl-[0.687rem] pr-[0.875rem] text-[0.75rem]">
                    <div className="flex-1 flex flex-col items-end justify-start gap-[0.062rem]">
                      <div className="self-stretch flex flex-row items-start justify-end py-[0rem] pl-[0.437rem] pr-[0.375rem]">
                        <div className="flex-1 flex flex-row items-start justify-between gap-[1.25rem]">
                          <img
                            className="h-[0.625rem] w-[0.625rem] relative object-cover z-[1]"
                            loading="lazy"
                            alt=""
                            src="/src/assets/actions--toggle--favorite--241@2x.png"
                          />
                          <img
                            className="h-[0.625rem] w-[0.625rem] relative object-cover z-[1]"
                            alt=""
                            src="/src/assets/actions--operations--chat--241@2x.png"
                          />
                          <img
                            className="h-[0.625rem] w-[0.625rem] relative z-[1]"
                            alt=""
                            src="/src/assets/actions--operations--sendaltfilled--24.svg"
                          />
                          <img
                            className="h-[0.625rem] w-[0.625rem] relative object-cover z-[1]"
                            alt=""
                            src="/src/assets/actions--operations--bookmark--24@2x.png"
                          />
                        </div>
                      </div>
                      <div className="self-stretch flex flex-row items-start justify-between gap-[1.25rem]">
                        <div className="relative inline-block min-w-[1.5rem] z-[1]">
                          800
                        </div>
                        <div className="relative inline-block min-w-[1.375rem] z-[1]">
                          150
                        </div>
                        <div className="relative inline-block min-w-[1.063rem] z-[1]">
                          30
                        </div>
                        <div className="relative inline-block min-w-[1.5rem] z-[1]">
                          250
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="flex flex-col items-start justify-start pt-[7.5rem] px-[0rem] pb-[0rem]">
                  <img
                    className="w-[1.25rem] h-[1.25rem] relative z-[1]"
                    loading="lazy"
                    alt=""
                    src="/src/assets/actions--navigation--chevronright--20.svg"
                  />
                </div>
              </div>
            </div>
            <div className="flex-1 rounded-[20px] bg-[#f0f4f9] flex flex-row items-start justify-start pt-[1.937rem] px-[2.812rem] pb-[3.375rem] box-border gap-[1.5rem] min-w-[21.25rem] max-w-full z-[1] text-center text-[#3c3c3c] mq750:flex-wrap mq750:pl-[1.375rem] mq750:pr-[1.375rem] mq750:box-border mq750:min-w-full">
              <div className="h-[20rem] w-[32.688rem] relative rounded-[20px] bg-[#f0f4f9] hidden max-w-full" />
              <div className="flex flex-col items-start justify-start gap-[4.375rem] mq750:flex-1">
                <div className="self-stretch flex flex-col items-start justify-start gap-[0.437rem]">
                  <div className="self-stretch flex flex-row items-start justify-start gap-[1rem]">
                    <b className="relative inline-block min-w-[3.375rem] z-[1]">
                      10 100
                    </b>
                    <b className="flex-1 relative z-[1]">Охват аккаунтів</b>
                  </div>
                  <div className="flex flex-row items-start justify-start py-[0rem] px-[0.375rem]">
                    <div className="w-[7.75rem] flex flex-row items-start justify-start gap-[1.125rem]">
                      <b className="relative inline-block min-w-[3rem] z-[1]">
                        9 050
                      </b>
                      <b className="flex-1 relative z-[1]">Покази</b>
                    </div>
                  </div>
                </div>
                <div className="flex flex-col items-start justify-start gap-[1.125rem] text-left text-[0.875rem] text-[#444]">
                  <div className="flex flex-row items-start justify-start gap-[0.625rem]">
                    <div className="h-[1.375rem] w-[1.375rem] relative rounded-[50%] bg-[#f3ae5f] z-[1]" />
                    <div className="flex flex-col items-start justify-start pt-[0.25rem] px-[0rem] pb-[0rem]">
                      <div className="relative inline-block min-w-[7.75rem] z-[1]">
                        Підписники 8 000
                      </div>
                    </div>
                  </div>
                  <div className="flex flex-row items-start justify-start gap-[0.625rem]">
                    <div className="h-[1.375rem] w-[1.375rem] relative rounded-[50%] bg-[#0b87ba] z-[1]" />
                    <div className="flex flex-col items-start justify-start pt-[0.312rem] px-[0rem] pb-[0rem]">
                      <div className="relative z-[1]">Непідписники 4 090</div>
                    </div>
                  </div>
                </div>
              </div>
              <div className="flex flex-col items-start justify-start pt-[3.562rem] px-[0rem] pb-[0rem] mq750:flex-1">
                <img
                  className="w-[11.125rem] h-[11.125rem] relative z-[1] mq750:self-stretch mq750:w-auto"
                  loading="lazy"
                  alt=""
                  src="/src/assets/group-1071.svg"
                />
              </div>
            </div>
          </div>
        </div>
        <ProfileStatisticsContainer
          propFlex="unset"
          propAlignSelf="stretch"
          propHeight="79.375rem"
          propLeft="-2.812rem"
          bG="/bg2.svg"
        />
      </div>
    );
  }
);

export default FrameComponent4;
