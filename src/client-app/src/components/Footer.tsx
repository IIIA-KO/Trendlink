import { FunctionComponent } from "react";
import Component3 from "./MenuWidjets";
import {ClassNameType} from "../types/ClassNameType";

const Footer: FunctionComponent<ClassNameType> = ({ className = "" }) => {
  return (
    <div
      className={`w-[344px] !m-[0] absolute top-[190px] left-[-180px] flex flex-row items-start justify-start max-w-full text-left text-xs text-text font-inter ${className}`}
    >
      
      <img
        className="h-[140px] w-[120px] absolute !m-[0] left-[300px] right-[1px] bottom-[235px] rounded-8xs object-cover z-[1]"
        loading="lazy"
        alt=""
        src="/src/assets/rectangle-57@2x.png"
      />
      <div className="absolute !m-[0] left-[320px] right-[80px] bottom-[205px] inline-block min-w-[26px] z-[1]">
        6.3К
      </div>
      <img
        className="h-2.5 w-2.5 absolute !m-[0] left-[300px] right-[111px] bottom-[208px] object-cover z-[1]"
        loading="lazy"
        alt=""
        src="/src/assets/actions--toggle--favorite--241@2x.png"
      />
      <div className="absolute !m-[0] left-[320px] right-[85px] bottom-[186px] inline-block min-w-[21px] z-[1]">
        150
      </div>
      <img
        className="h-2.5 w-2.5 absolute left-[300px] !m-[0] right-[111px] bottom-[188px] object-cover z-[1]"
        loading="lazy"
        alt=""
        src="/src/assets/actions--operations--bookmark--24@2x.png"
      />
      <img
        className="h-8 w-8 absolute !m-[0] right-[134px] bottom-[289px] z-[1]"
        loading="lazy"
        alt=""
        src="/src/assets/actions--navigation--chevronleft--32.svg"
      />
      <Component3
        showIcon
        prop1="статистика"
        div
        iconVisible
        prop3="календар"
        div1
        iconVisible1
        prop5="відгуки"
        div2
        iconVisible2
        prop7="новини"
        div3
        iconVisible3
        prop9="Сповіщення"
        div4
        iconVisible4
        prop11="Збережені"
        div5
        iconVisible5
        prop13="Умови співпраці"
        div6
        iconVisible6
        prop15="Налаштування"
        div7
        iconVisible7
        prop17="служба підтримки"
        div8
      />
    </div>
  );
};

export default Footer;
