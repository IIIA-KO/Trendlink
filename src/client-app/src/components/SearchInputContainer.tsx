import { FunctionComponent, memo } from "react";
import ProfileNavigation from "./ProfileNavigation";
import Component7 from "./Component7";
import ProfileStatisticsContainer from "./ProfileStatisticsContainer";
import {ClassNameType} from "../types/ClassNameType";

const SearchInputContainer: FunctionComponent<ClassNameType> = memo(
  ({ className = "" }) => {
    return (
      <section
        className={`flex-1 flex flex-col items-start justify-start gap-[3.531rem] max-w-full text-left text-[0.75rem] text-[#7c7c7c] font-[Inter] mq450:gap-[1.75rem] ${className}`}
      >
        <ProfileNavigation
          prop="Аудиторія"
          prop1="Публікації"
          prop2="Історії"
          prop3="Reels"
          prop4="Залучення"
          propLeft="unset"
          propLeft1="unset"
          propLeft2="unset"
          propLeft3="unset"
          propLeft4="unset"
          propBorder="1px solid #c0bebe"
          propBorder1="1px solid #c0bebe"
          propBorder2="unset"
          propBorder3="1px solid #c0bebe"
          propBorder4="1px solid #c0bebe"
          propBackgroundColor="unset"
          propBackgroundColor1="unset"
          propBackgroundColor2="#009ea0"
          propBackgroundColor3="unset"
          propBackgroundColor4="unset"
          propColor="#3c3c3c"
          propColor1="#3c3c3c"
          propColor2="#fff"
          propColor3="#3c3c3c"
          propColor4="#3c3c3c"
          propMinWidth="4.313rem"
          propMinWidth1="4.438rem"
          propMinWidth2="unset"
          propMinWidth3="unset"
          propMinWidth4="unset"
          component7Top="unset"
          component7Top1="unset"
          component7Top2="unset"
          component7Top3="unset"
          component7Top4="unset"
          component7Padding="0.562rem 3.375rem 0.562rem 3.562rem"
          component7Padding1="0.562rem 3.312rem 0.562rem 3.5rem"
          component7Padding2="0.687rem 4.312rem"
          component7Padding3="0.562rem 4.375rem 0.562rem 4.562rem"
          component7Padding4="0.562rem 3.312rem"
          component7Height="unset"
          component7Height1="unset"
          component7Height2="unset"
          component7Height3="unset"
          component7Height4="unset"
          component7Position="unset"
          component7Position1="unset"
          component7Position2="unset"
          component7Position3="unset"
          component7Position4="unset"
          divDisplay="inline-block"
          divDisplay1="inline-block"
          divDisplay2="unset"
          divDisplay3="unset"
          divDisplay4="unset"
        />
        <div className="self-stretch flex flex-col items-start justify-start gap-[1.875rem] max-w-full">
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
                        src="/src/assets/icons/search-icon.svg"
                      />
                      <div className="relative uppercase hidden">Пошук</div>
                    </div>
                    <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.093rem] px-[2.625rem] pb-[1.1rem] gap-[1.875rem]">
                      <img
                        className="h-[2.181rem] w-[2.188rem] relative overflow-hidden shrink-0"
                        loading="lazy"
                        alt=""
                        src="/src/assets/icons/chat-icon.svg"
                      />
                      <div className="relative uppercase hidden">Чат</div>
                    </div>
                    <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.168rem] px-[2.625rem] pb-[1.175rem] gap-[1.875rem]">
                      <img
                        className="h-[2.031rem] w-[2.188rem] relative overflow-hidden shrink-0"
                        loading="lazy"
                        alt=""
                        src="/src/assets/icons/statistics-icon.svg"
                      />
                      <div className="relative uppercase hidden">
                        статистика
                      </div>
                    </div>
                    <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start py-[1.168rem] px-[2.625rem] gap-[1.875rem]">
                      <img
                        className="h-[2.038rem] w-[2.188rem] relative overflow-hidden shrink-0"
                        loading="lazy"
                        alt=""
                        src="/src/assets/icons/calendar-icon.svg"
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
                        src="/src/assets/icons/news-icon.svg"
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
                      <div className="relative uppercase hidden">
                        Сповіщення
                      </div>
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
                            alt=""
                            src="/src/assets/skilliconsinstagram2.svg"
                          />
                        </div>
                        <div className="relative inline-block min-w-[6.688rem] z-[1]">
                          1,8% залучення
                        </div>
                      </div>
                    </div>
                    <img
                      className="self-stretch h-[9.813rem] relative max-w-full overflow-hidden shrink-0 object-cover z-[1]"
                      loading="lazy"
                      alt=""
                      src="/src/assets/group-117@2x.png"
                    />
                    <div className="self-stretch flex flex-row items-start justify-start py-[0rem] pl-[0.687rem] pr-[0.875rem] text-[0.75rem]">
                      <div className="flex-1 flex flex-col items-end justify-start gap-[0.062rem]">
                        <div className="self-stretch flex flex-row items-start justify-end py-[0rem] pl-[0.437rem] pr-[0.375rem]">
                          <div className="flex-1 flex flex-row items-start justify-between gap-[1.25rem]">
                            <img
                              className="h-[0.625rem] w-[0.625rem] relative object-cover z-[1]"
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
                    <div className="self-stretch h-[16.875rem] relative rounded-[10px] border-[#d9d9d9] border-[1px] border-solid box-border hidden" />
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
              <div className="flex-1 rounded-[20px] bg-[#f0f4f9] flex flex-row items-start justify-start pt-[1.937rem] px-[2.75rem] pb-[3.375rem] box-border gap-[1.5rem] min-w-[21.25rem] max-w-full z-[1] text-center text-[#3c3c3c] mq750:flex-wrap mq750:pl-[1.375rem] mq750:pr-[1.375rem] mq750:box-border mq750:min-w-full">
                <div className="h-[20rem] w-[32.688rem] relative rounded-[20px] bg-[#f0f4f9] hidden max-w-full" />
                <div className="flex flex-col items-start justify-start gap-[4.375rem] mq750:flex-1">
                  <div className="self-stretch flex flex-col items-start justify-start gap-[0.437rem]">
                    <div className="self-stretch flex flex-row items-start justify-start gap-[0.937rem]">
                      <b className="relative inline-block min-w-[3.5rem] z-[1]">
                        12 090
                      </b>
                      <b className="flex-1 relative z-[1]">Охват аккаунтів</b>
                    </div>
                    <div className="flex flex-row items-start justify-start gap-[1.062rem]">
                      <b className="flex-1 relative inline-block min-w-[3.5rem] z-[1]">
                        13 050
                      </b>
                      <b className="flex-1 relative z-[1]">Покази</b>
                    </div>
                  </div>
                  <div className="flex flex-row items-start justify-start py-[0rem] px-[0.062rem] text-left text-[0.875rem] text-[#444]">
                    <div className="flex flex-col items-start justify-start gap-[1.125rem]">
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
                          <div className="relative z-[1]">
                            Непідписники 4 090
                          </div>
                        </div>
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
          <div className="self-stretch flex flex-row items-start justify-start py-[0rem] pl-[0.062rem] pr-[0rem] box-border max-w-full text-center text-[1rem] text-[#3c3c3c]">
            <ProfileStatisticsContainer bG="/bg1.svg" />
          </div>
        </div>
        <div className="self-stretch flex flex-row items-start justify-start py-[0rem] pl-[0.062rem] pr-[0rem] box-border max-w-full text-[0.875rem] text-[#3c3c3c]">
          <div className="flex-1 flex flex-col items-start justify-start gap-[0.625rem] max-w-full">
            <div className="self-stretch flex flex-row items-start justify-end py-[0rem] px-[4.312rem] box-border max-w-full mq750:pl-[2.125rem] mq750:pr-[2.125rem] mq750:box-border">
              <div className="w-[31.813rem] flex flex-row items-start justify-between gap-[1.25rem] max-w-full mq750:flex-wrap">
                <div className="flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[0.312rem]">
                  <b className="relative inline-block min-w-[3.188rem] z-[1]">
                    Всього
                  </b>
                </div>
                <div className="flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[0.187rem]">
                  <b className="relative inline-block min-w-[4.188rem] z-[1]">
                    Рекламні
                  </b>
                </div>
                <b className="relative inline-block min-w-[5.25rem] z-[1]">
                  Коментарів
                </b>
                <div className="flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[0.312rem]">
                  <b className="relative z-[1]">Охват</b>
                </div>
                <b className="relative inline-block min-w-[5.5rem] z-[1]">
                  Підписників
                </b>
              </div>
            </div>
            <div className="self-stretch flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.437rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="w-[55.25rem] flex flex-row items-start justify-start pt-[0rem] px-[1.75rem] pb-[0.375rem] box-border max-w-full">
              <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq750:flex-wrap">
                <div className="relative inline-block min-w-[4.25rem] z-[1]">{`1 березня `}</div>
                <div className="flex flex-row items-start justify-start gap-[3.687rem] max-w-full mq750:gap-[1.813rem] mq750:flex-wrap">
                  <div className="w-[3.438rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative z-[1]">5</div>
                  </div>
                  <div className="w-[3.063rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[0.5rem] z-[1]">
                      -
                    </div>
                  </div>
                  <div className="relative z-[1]">+35</div>
                  <div className="w-[4rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative z-[1]">1,5к</div>
                  </div>
                  <b className="relative z-[1]">+195</b>
                </div>
              </div>
            </div>
            <div className="self-stretch h-[0.281rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.25rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="w-[55.313rem] flex flex-row items-start justify-start pt-[0rem] px-[1.75rem] pb-[0.375rem] box-border max-w-full">
              <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq750:flex-wrap">
                <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                  <div className="relative inline-block min-w-[4.375rem] z-[1]">
                    2 березня
                  </div>
                </div>
                <div className="flex flex-row items-start justify-start gap-[3.687rem] max-w-full mq750:gap-[1.813rem] mq750:flex-wrap">
                  <div className="w-[3.75rem] flex flex-col items-start justify-start pt-[0.187rem] pb-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[1.063rem] z-[1]">
                      15
                    </div>
                  </div>
                  <div className="w-[3.188rem] flex flex-col items-start justify-start pt-[0.187rem] pb-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative z-[1]">5</div>
                  </div>
                  <div className="relative z-[1]">+80</div>
                  <div className="w-[3.875rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative z-[1]">1,8к</div>
                  </div>
                  <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                    <b className="relative inline-block min-w-[2.438rem] z-[1]">
                      +200
                    </b>
                  </div>
                </div>
              </div>
            </div>
            <div className="self-stretch h-[0.281rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.25rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="w-[55.25rem] flex flex-row items-start justify-start pt-[0rem] px-[1.75rem] pb-[0.375rem] box-border max-w-full">
              <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq750:flex-wrap">
                <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                  <div className="relative z-[1]">3 березня</div>
                </div>
                <div className="w-[29.125rem] flex flex-row items-start justify-start gap-[4.312rem] max-w-full mq750:gap-[2.125rem] mq750:flex-wrap">
                  <div className="flex-[0.5556] flex flex-col items-start justify-start pt-[0.187rem] pb-[0rem] pl-[0rem] pr-[1.25rem] box-border min-w-[1.813rem] mq450:flex-1">
                    <div className="relative z-[1]">8</div>
                  </div>
                  <div className="w-[2rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[0.5rem] z-[1]">
                      -
                    </div>
                  </div>
                  <div className="flex-1 relative inline-block min-w-[1.563rem] z-[1]">
                    +100
                  </div>
                  <div className="flex-[0.5238] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border min-w-[1.813rem] mq450:flex-1">
                    <div className="relative inline-block min-w-[1.125rem] z-[1]">
                      2к
                    </div>
                  </div>
                  <b className="flex-1 relative inline-block min-w-[1.563rem] z-[1]">
                    +250
                  </b>
                </div>
              </div>
            </div>
            <div className="self-stretch h-[0.281rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.25rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="w-[55.25rem] flex flex-row items-start justify-start pt-[0rem] px-[1.75rem] pb-[0.375rem] box-border max-w-full">
              <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq750:flex-wrap">
                <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                  <div className="relative inline-block min-w-[4.438rem] z-[1]">
                    4 березня
                  </div>
                </div>
                <div className="flex flex-row items-start justify-start gap-[4.375rem] max-w-full mq750:gap-[2.188rem] mq750:flex-wrap">
                  <div className="w-[3.063rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[1.063rem] z-[1]">
                      10
                    </div>
                  </div>
                  <div className="w-[3.625rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[0.688rem] z-[1]">
                      4
                    </div>
                  </div>
                  <div className="relative z-[1] mq750:w-full mq750:h-[0.625rem]">
                    5
                  </div>
                  <div className="w-[3rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[1rem] z-[1]">
                      1к
                    </div>
                  </div>
                  <b className="relative inline-block min-w-[1.875rem] z-[1]">
                    +50
                  </b>
                </div>
              </div>
            </div>
            <div className="self-stretch h-[0.281rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.25rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="w-[55.25rem] flex flex-row items-start justify-start pt-[0rem] px-[1.75rem] pb-[0.375rem] box-border max-w-full">
              <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq750:flex-wrap">
                <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                  <div className="relative inline-block min-w-[4.375rem] z-[1]">
                    5 березня
                  </div>
                </div>
                <div className="flex flex-row items-start justify-start gap-[3.812rem] max-w-full mq750:gap-[1.875rem] mq750:flex-wrap">
                  <div className="w-[3.625rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[1.063rem] z-[1]">
                      10
                    </div>
                  </div>
                  <div className="w-[3.625rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[0.625rem] z-[1]">
                      2
                    </div>
                  </div>
                  <div className="relative inline-block min-w-[1.188rem] z-[1]">
                    20
                  </div>
                  <div className="w-[4.125rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative z-[1]">1,1к</div>
                  </div>
                  <b className="relative inline-block min-w-[1.875rem] z-[1]">
                    +60
                  </b>
                </div>
              </div>
            </div>
            <div className="self-stretch flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.437rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="w-[55.25rem] flex flex-row items-start justify-start pt-[0rem] px-[1.75rem] pb-[0.187rem] box-border max-w-full">
              <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq750:flex-wrap">
                <div className="relative z-[1]">6 березня</div>
                <div className="flex flex-row items-start justify-start gap-[4.25rem] max-w-full mq750:gap-[2.125rem] mq750:flex-wrap">
                  <div className="w-[3.188rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[1.063rem] z-[1]">
                      10
                    </div>
                  </div>
                  <div className="w-[2.188rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative z-[1]">6</div>
                  </div>
                  <div className="relative inline-block min-w-[2.188rem] z-[1]">
                    +100
                  </div>
                  <div className="w-[2.813rem] flex flex-col items-start justify-start pt-[0.187rem] pb-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                    <div className="relative inline-block min-w-[1.125rem] z-[1]">
                      2к
                    </div>
                  </div>
                  <b className="relative z-[1]">+100</b>
                </div>
              </div>
            </div>
            <div className="self-stretch h-[0.281rem] flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.25rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="w-[55.25rem] flex flex-row items-start justify-start pt-[0rem] px-[1.75rem] pb-[0.25rem] box-border max-w-full">
              <div className="flex-1 flex flex-row items-start justify-start relative max-w-full">
                <div className="h-[24.031rem] w-[0.031rem] absolute !m-[0] top-[-21.75rem] left-[18.813rem] border-[#c0bebe] border-r-[0.5px] border-solid box-border z-[2]" />
                <div className="h-[24.031rem] w-[0.031rem] absolute !m-[0] top-[-21.75rem] left-[24.438rem] border-[#c0bebe] border-r-[0.5px] border-solid box-border z-[2]" />
                <div className="h-[24.031rem] w-[0.031rem] absolute !m-[0] top-[-21.75rem] right-[19.844rem] border-[#c0bebe] border-r-[0.5px] border-solid box-border z-[2]" />
                <div className="h-[24.031rem] w-[0.031rem] absolute !m-[0] top-[-21.75rem] right-[12.406rem] border-[#c0bebe] border-r-[0.5px] border-solid box-border z-[2]" />
                <div className="h-[24.031rem] w-[0.031rem] absolute !m-[0] top-[-21.75rem] right-[6.781rem] border-[#c0bebe] border-r-[0.5px] border-solid box-border z-[2]" />
                <div className="flex-1 flex flex-row items-start justify-between max-w-full gap-[1.25rem] mq750:flex-wrap">
                  <div className="flex flex-col items-start justify-start pt-[0.187rem] px-[0rem] pb-[0rem]">
                    <div className="relative z-[1]">7 березня</div>
                  </div>
                  <div className="flex flex-row items-start justify-start gap-[4.375rem] max-w-full mq750:gap-[2.188rem] mq750:flex-wrap">
                    <div className="w-[2.75rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                      <div className="relative z-[1]">0</div>
                    </div>
                    <div className="w-[3.625rem] flex flex-col items-start justify-start py-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                      <div className="relative inline-block min-w-[0.5rem] z-[1]">
                        -
                      </div>
                    </div>
                    <div className="relative inline-block min-w-[0.5rem] z-[1] mq750:w-full mq750:h-[0.5rem]">
                      -
                    </div>
                    <div className="w-[3rem] flex flex-col items-start justify-start pt-[0.312rem] pb-[0rem] pl-[0rem] pr-[1.25rem] box-border">
                      <div className="relative inline-block min-w-[1rem] z-[1]">
                        1к
                      </div>
                    </div>
                    <b className="relative z-[1]">+40</b>
                  </div>
                </div>
              </div>
            </div>
            <div className="self-stretch flex flex-row items-start justify-start pt-[0rem] px-[0rem] pb-[0.437rem] box-border max-w-full">
              <div className="self-stretch flex-1 relative border-[#c0bebe] border-t-[0.5px] border-solid box-border max-w-full z-[1]" />
            </div>
            <div className="flex flex-row items-start justify-start py-[0rem] pl-[3.562rem] pr-[3.5rem]">
              <div className="relative inline-block min-w-[2rem] z-[1]">
                Далі
              </div>
            </div>
          </div>
        </div>
      </section>
    );
  }
);

export default SearchInputContainer;
