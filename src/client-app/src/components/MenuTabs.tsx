import { FunctionComponent, memo } from "react";
import Component5 from "./Component5 (2)";
import FrameComponent2 from "./FrameComponent2";
import Component7 from "./Component7";
import {ClassNameType} from "../types/ClassNameType";

const MenuTabs: FunctionComponent<ClassNameType> = memo(({ className = "" }) => {
  return (
    <div
      className={`h-[56.688rem] w-[70.063rem] fixed relative max-w-full text-left text-[0.875rem] text-[#3c3c3c] font-[Inter] mq750:h-auto mq750:min-h-[907] ${className}`}
    >
      <div className="absolute top-[11.25rem] left-[23.813rem] border-[#c0bebe] border-b-[1px] border-solid box-border h-[2.438rem] overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[3.937rem] pr-[3.875rem] z-[1]">
        <div className="relative inline-block min-w-[3.563rem]"> Відгуки</div>
      </div>
      <div className="absolute top-[11.25rem] left-[12.188rem] border-[#c0bebe] border-b-[1px] border-solid box-border h-[2.438rem] overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[4.375rem] pr-[4.312rem] z-[3]">
        <div className="relative inline-block min-w-[2.688rem]">Огляд</div>
      </div>
      <div className="absolute top-[11.25rem] left-[35.438rem] border-[#009ea0] border-b-[2px] border-solid box-border h-[2.438rem] overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.5rem] pl-[3.187rem] pr-[3.125rem] z-[1]">
        <div className="relative inline-block min-w-[5.063rem]">Статистика</div>
      </div>
      <div className="absolute top-[11.25rem] left-[47.063rem] border-[#c0bebe] border-b-[1px] border-solid box-border h-[2.438rem] overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[3.625rem] pr-[3.562rem] z-[1] text-[#000]">
        <div className="relative">Написати</div>
      </div>
      <div className="absolute top-[11.25rem] left-[58.688rem] border-[#c0bebe] border-b-[1px] border-solid box-border h-[2.438rem] overflow-hidden flex flex-row items-start justify-start pt-[0.687rem] pb-[0.562rem] pl-[2.187rem] pr-[2.125rem] z-[1]">
        <div className="relative inline-block min-w-[7.063rem]">
          Умови співпраці
        </div>
      </div>
      <div className="absolute top-[2rem]">
      <Component5
        propLeft="12.125rem"
        propBorder="unset"
        propBackgroundColor="#009ea0"
        prop="Аудиторія"
        propColor="#fff"
        propMinWidth="4.313rem"
        component7Top="16.813rem"
        component7Padding="0.687rem 3.5rem 0.687rem 3.562rem"
        component7Height="unset"
        component7Position="absolute"
        divDisplay="inline-block"
      />
      <Component5
        propLeft="23.75rem"
        propBorder="1px solid #c0bebe"
        propBackgroundColor="unset"
        prop="Публікації"
        propColor="#3c3c3c"
        propMinWidth="4.438rem"
        component7Top="16.813rem"
        component7Padding="0.562rem 3.312rem 0.562rem 3.5rem"
        component7Height="2.438rem"
        component7Position="absolute"
        divDisplay="inline-block"
      />
      <Component5
        propLeft="35.375rem"
        propBorder="1px solid #c0bebe"
        propBackgroundColor="unset"
        prop="Історії"
        propColor="#3c3c3c"
        propMinWidth="unset"
        component7Top="16.813rem"
        component7Padding="0.562rem 4.25rem"
        component7Height="2.438rem"
        component7Position="absolute"
        divDisplay="unset"
      />
      <Component5
        propLeft="47rem"
        propBorder="1px solid #c0bebe"
        propBackgroundColor="unset"
        prop="Reels"
        propColor="#3c3c3c"
        propMinWidth="unset"
        component7Top="16.813rem"
        component7Padding="0.562rem 4.375rem 0.562rem 4.562rem"
        component7Height="2.438rem"
        component7Position="absolute"
        divDisplay="unset"
      />
      <Component5
        propLeft="58.625rem"
        propBorder="1px solid #c0bebe"
        propBackgroundColor="unset"
        prop="Залучення"
        propColor="#3c3c3c"
        propMinWidth="unset"
        component7Top="16.813rem"
        component7Padding="0.562rem 3.312rem"
        component7Height="2.438rem"
        component7Position="absolute"
        divDisplay="unset"
      />
      </div>
      <div className="absolute top-[22.688rem] left-[64.313rem] text-[0.75rem] [text-decoration:underline] text-[#7c7c7c] inline-block w-[5.813rem] h-[0.938rem] min-w-[5.813rem] z-[1]">
        Зберегти в PDF
      </div>
      <div className="absolute top-[24.875rem] left-[12.125rem] rounded-[20px] bg-[#f0f4f9] w-[37.375rem] flex flex-col items-start justify-start py-[1.75rem] pl-[3.437rem] pr-[3.312rem] box-border gap-[1.343rem] max-w-full z-[3] text-[0.75rem]">
        <div className="w-[37.375rem] h-[14.563rem] relative rounded-[20px] bg-[#f0f4f9] hidden max-w-full" />
        <b className="relative text-[1rem] uppercase inline-block min-w-[1.75rem] z-[1]">
          Вік
        </b>
        <div className="self-stretch flex flex-row items-start justify-start py-[0rem] px-[0.062rem] box-border max-w-full">
          <div className="flex-1 flex flex-row items-start justify-start gap-[0.75rem] max-w-full mq750:flex-wrap">
            <div className="flex-1 flex flex-col items-start justify-start pt-[0.5rem] px-[0rem] pb-[0rem] box-border min-w-[18.25rem] max-w-full">
              <div className="self-stretch h-[0.031rem] relative border-[#c0bebe] border-t-[0.5px] border-solid box-border z-[1]" />
            </div>
            <div className="relative inline-block min-w-[1.75rem] z-[1]">
              60%
            </div>
          </div>
        </div>
        <div className="self-stretch flex flex-row items-start justify-start py-[0rem] pl-[0.062rem] pr-[0rem] box-border max-w-full">
          <div className="flex-1 flex flex-row items-start justify-start gap-[0.75rem] max-w-full mq750:flex-wrap">
            <div className="flex-1 flex flex-col items-start justify-start pt-[0.5rem] px-[0rem] pb-[0rem] box-border max-w-full">
              <div className="self-stretch flex flex-col items-start justify-start gap-[2.187rem] max-w-full">
                <div className="self-stretch h-[0.031rem] relative border-[#c0bebe] border-t-[0.5px] border-solid box-border z-[1]" />
                <div className="self-stretch flex flex-col items-end justify-start gap-[0.468rem] max-w-full">
                  <div className="self-stretch flex flex-col items-start justify-start">
                    <div className="self-stretch h-[0.031rem] relative border-[#c0bebe] border-t-[0.5px] border-solid box-border z-[1]" />
                    <div className="self-stretch h-[2.219rem] relative">
                      <div className="absolute top-[2.188rem] left-[0rem] border-[#c0bebe] border-t-[0.5px] border-solid box-border w-[28.094rem] h-[0.031rem] z-[1]" />
                      <div className="absolute top-[1.625rem] left-[1.438rem] bg-[#0b87ba] w-[1.375rem] h-[0.563rem] z-[2]" />
                      <div className="absolute top-[1.938rem] left-[25.375rem] bg-[#0b87ba] w-[1.375rem] h-[0.25rem] z-[2]" />
                      <div className="absolute top-[0rem] left-[7.375rem] bg-[#0b87ba] w-[1.375rem] h-[2.188rem] z-[2]" />
                      <div className="absolute top-[0.25rem] left-[19.375rem] bg-[#0b87ba] w-[1.375rem] h-[1.938rem] z-[2]" />
                      <div className="absolute top-[-3.375rem] left-[13.313rem] bg-[#0b87ba] w-[1.375rem] h-[5.563rem] z-[2]" />
                    </div>
                  </div>
                  <div className="self-stretch flex flex-row items-start justify-end py-[0rem] pl-[1.125rem] pr-[0.562rem] box-border max-w-full">
                    <div className="w-[26.375rem] flex flex-row items-start justify-between gap-[1.25rem] max-w-full mq750:flex-wrap">
                      <div className="relative inline-block min-w-[2.625rem] z-[1]">
                        18-24р
                      </div>
                      <div className="relative z-[1]">25-34р</div>
                      <div className="relative inline-block min-w-[2.813rem] z-[1]">
                        35-44р
                      </div>
                      <div className="relative z-[1]">45-54р</div>
                      <div className="relative z-[1]">55-64р</div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div className="flex flex-col items-start justify-start gap-[1.312rem]">
              <div className="relative inline-block min-w-[1.813rem] z-[1]">
                40%
              </div>
              <div className="relative inline-block min-w-[1.75rem] z-[1]">
                20%
              </div>
              <div className="relative inline-block min-w-[1.313rem] z-[1]">
                0%
              </div>
            </div>
          </div>
        </div>
      </div>
      <FrameComponent2 bG="/bg3.svg" />
      <div className="absolute top-[0rem] left-[0rem] w-[21.5rem] flex flex-row items-start justify-start max-w-full text-[1rem] text-[#fff]">
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
            <div className="relative uppercase hidden">Умови співпраці</div>
          </div>
          <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.193rem] px-[2.625rem] pb-[1.2rem] gap-[1.875rem]">
            <img
              className="h-[1.981rem] w-[2.188rem] relative overflow-hidden shrink-0"
              loading="lazy"
              alt=""
              src="/src/assets/-9.svg"
            />
            <div className="relative uppercase hidden">Налаштування</div>
          </div>
          <div className="self-stretch bg-[rgba(38,46,46,0)] overflow-hidden flex flex-row items-start justify-start pt-[1.112rem] px-[2.625rem] pb-[1.118rem] gap-[1.875rem] z-[1]">
            <img
              className="h-[2.144rem] w-[2.188rem] relative overflow-hidden shrink-0"
              loading="lazy"
              alt=""
              src="/src/assets/--3.svg"
            />
            <div className="relative uppercase hidden">служба підтримки</div>
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
    </div>
  );
});

export default MenuTabs;
