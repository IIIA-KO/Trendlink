import { FunctionComponent, useMemo, type CSSProperties } from "react";
import "antd/dist/reset.css";
import { Select } from "antd";

export type Component3Type = {
  className?: string;
  showIcon?: boolean;
  prop1?: string;
  div?: boolean;
  iconVisible?: boolean;
  prop3?: string;
  div1?: boolean;
  iconVisible1?: boolean;
  prop5?: string;
  div2?: boolean;
  iconVisible2?: boolean;
  prop7?: string;
  div3?: boolean;
  iconVisible3?: boolean;
  prop9?: string;
  div4?: boolean;
  iconVisible4?: boolean;
  prop11?: string;
  div5?: boolean;
  iconVisible5?: boolean;
  prop13?: string;
  div6?: boolean;
  iconVisible6?: boolean;
  prop15?: string;
  div7?: boolean;
  iconVisible7?: boolean;
  prop17?: string;
  div8?: boolean;

  /** Style props */
  propFlex?: CSSProperties["flex"];
  propWidth?: CSSProperties["width"];
  propMargin?: CSSProperties["margin"];
  propPosition?: CSSProperties["position"];
  propTop?: CSSProperties["top"];
  propLeft?: CSSProperties["left"];
  navigationOptionsBorderRadius?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor?: CSSProperties["backgroundColor"];
  navigationOptionsOverflow?: CSSProperties["overflow"];
  navigationOptionsDisplay?: CSSProperties["display"];
  navigationOptionsFlexDirection?: CSSProperties["flexDirection"];
  navigationOptionsPadding?: CSSProperties["padding"];
  navigationOptionsGap?: CSSProperties["gap"];
  navigationOptionsAlignSelf?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius1?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor1?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay1?: CSSProperties["display"];
  navigationOptionsFlexDirection1?: CSSProperties["flexDirection"];
  navigationOptionsPadding1?: CSSProperties["padding"];
  navigationOptionsGap1?: CSSProperties["gap"];
  navigationOptionsHeight?: CSSProperties["height"];
  navigationOptionsWidth?: CSSProperties["width"];
  navigationOptionsPosition?: CSSProperties["position"];
  iconHeight?: CSSProperties["height"];
  iconOverflow?: CSSProperties["overflow"];
  iconFontSize?: CSSProperties["fontSize"];
  iconTextTransform?: CSSProperties["textTransform"];
  iconFontFamily?: CSSProperties["fontFamily"];
  iconColor?: CSSProperties["color"];
  iconTextAlign?: CSSProperties["textAlign"];
  divWidth?: CSSProperties["width"];
  divPosition?: CSSProperties["position"];
  divFontSize?: CSSProperties["fontSize"];
  divTextTransform?: CSSProperties["textTransform"];
  divFontFamily?: CSSProperties["fontFamily"];
  divColor?: CSSProperties["color"];
  divTextAlign?: CSSProperties["textAlign"];
  divAlignSelf?: CSSProperties["alignSelf"];
  divBorderRadius?: CSSProperties["borderRadius"];
  divBackgroundColor?: CSSProperties["backgroundColor"];
  divOverflow?: CSSProperties["overflow"];
  divFlexDirection?: CSSProperties["flexDirection"];
  divPadding?: CSSProperties["padding"];
  divGap?: CSSProperties["gap"];
  navigationOptionsAlignSelf1?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius2?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor2?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay2?: CSSProperties["display"];
  navigationOptionsFlexDirection2?: CSSProperties["flexDirection"];
  navigationOptionsPadding2?: CSSProperties["padding"];
  navigationOptionsGap2?: CSSProperties["gap"];
  navigationOptionsHeight1?: CSSProperties["height"];
  navigationOptionsWidth1?: CSSProperties["width"];
  navigationOptionsPosition1?: CSSProperties["position"];
  iconHeight1?: CSSProperties["height"];
  iconWidth?: CSSProperties["width"];
  iconOverflow1?: CSSProperties["overflow"];
  iconFontSize1?: CSSProperties["fontSize"];
  iconTextTransform1?: CSSProperties["textTransform"];
  iconFontFamily1?: CSSProperties["fontFamily"];
  iconColor1?: CSSProperties["color"];
  iconTextAlign1?: CSSProperties["textAlign"];
  divWidth1?: CSSProperties["width"];
  divPosition1?: CSSProperties["position"];
  divFontSize1?: CSSProperties["fontSize"];
  divTextTransform1?: CSSProperties["textTransform"];
  divFontFamily1?: CSSProperties["fontFamily"];
  divColor1?: CSSProperties["color"];
  divTextAlign1?: CSSProperties["textAlign"];
  divAlignSelf1?: CSSProperties["alignSelf"];
  divBorderRadius1?: CSSProperties["borderRadius"];
  divBackgroundColor1?: CSSProperties["backgroundColor"];
  divOverflow1?: CSSProperties["overflow"];
  divFlexDirection1?: CSSProperties["flexDirection"];
  divPadding1?: CSSProperties["padding"];
  divGap1?: CSSProperties["gap"];
  navigationOptionsAlignSelf2?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius3?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor3?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay3?: CSSProperties["display"];
  navigationOptionsFlexDirection3?: CSSProperties["flexDirection"];
  navigationOptionsPadding3?: CSSProperties["padding"];
  navigationOptionsGap3?: CSSProperties["gap"];
  navigationOptionsHeight2?: CSSProperties["height"];
  navigationOptionsWidth2?: CSSProperties["width"];
  navigationOptionsPosition2?: CSSProperties["position"];
  iconHeight2?: CSSProperties["height"];
  iconWidth1?: CSSProperties["width"];
  iconOverflow2?: CSSProperties["overflow"];
  iconFontSize2?: CSSProperties["fontSize"];
  iconTextTransform2?: CSSProperties["textTransform"];
  iconFontFamily2?: CSSProperties["fontFamily"];
  iconColor2?: CSSProperties["color"];
  iconTextAlign2?: CSSProperties["textAlign"];
  divWidth2?: CSSProperties["width"];
  divPosition2?: CSSProperties["position"];
  divFontSize2?: CSSProperties["fontSize"];
  divTextTransform2?: CSSProperties["textTransform"];
  divFontFamily2?: CSSProperties["fontFamily"];
  divColor2?: CSSProperties["color"];
  divTextAlign2?: CSSProperties["textAlign"];
  divAlignSelf2?: CSSProperties["alignSelf"];
  divBorderRadius2?: CSSProperties["borderRadius"];
  divBackgroundColor2?: CSSProperties["backgroundColor"];
  divOverflow2?: CSSProperties["overflow"];
  divFlexDirection2?: CSSProperties["flexDirection"];
  divPadding2?: CSSProperties["padding"];
  divGap2?: CSSProperties["gap"];
  navigationOptionsAlignSelf3?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius4?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor4?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay4?: CSSProperties["display"];
  navigationOptionsFlexDirection4?: CSSProperties["flexDirection"];
  navigationOptionsPadding4?: CSSProperties["padding"];
  navigationOptionsGap4?: CSSProperties["gap"];
  navigationOptionsHeight3?: CSSProperties["height"];
  navigationOptionsWidth3?: CSSProperties["width"];
  navigationOptionsPosition3?: CSSProperties["position"];
  iconHeight3?: CSSProperties["height"];
  iconWidth2?: CSSProperties["width"];
  iconOverflow3?: CSSProperties["overflow"];
  iconFontSize3?: CSSProperties["fontSize"];
  iconTextTransform3?: CSSProperties["textTransform"];
  iconFontFamily3?: CSSProperties["fontFamily"];
  iconColor3?: CSSProperties["color"];
  iconTextAlign3?: CSSProperties["textAlign"];
  divWidth3?: CSSProperties["width"];
  divPosition3?: CSSProperties["position"];
  divFontSize3?: CSSProperties["fontSize"];
  divTextTransform3?: CSSProperties["textTransform"];
  divFontFamily3?: CSSProperties["fontFamily"];
  divColor3?: CSSProperties["color"];
  divTextAlign3?: CSSProperties["textAlign"];
  divAlignSelf3?: CSSProperties["alignSelf"];
  divBorderRadius3?: CSSProperties["borderRadius"];
  divBackgroundColor3?: CSSProperties["backgroundColor"];
  divOverflow3?: CSSProperties["overflow"];
  divFlexDirection3?: CSSProperties["flexDirection"];
  divPadding3?: CSSProperties["padding"];
  divGap3?: CSSProperties["gap"];
  navigationOptionsAlignSelf4?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius5?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor5?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay5?: CSSProperties["display"];
  navigationOptionsFlexDirection5?: CSSProperties["flexDirection"];
  navigationOptionsPadding5?: CSSProperties["padding"];
  navigationOptionsGap5?: CSSProperties["gap"];
  navigationOptionsHeight4?: CSSProperties["height"];
  navigationOptionsWidth4?: CSSProperties["width"];
  navigationOptionsPosition4?: CSSProperties["position"];
  iconHeight4?: CSSProperties["height"];
  iconWidth3?: CSSProperties["width"];
  iconOverflow4?: CSSProperties["overflow"];
  iconFontSize4?: CSSProperties["fontSize"];
  iconTextTransform4?: CSSProperties["textTransform"];
  iconFontFamily4?: CSSProperties["fontFamily"];
  iconColor4?: CSSProperties["color"];
  iconTextAlign4?: CSSProperties["textAlign"];
  divWidth4?: CSSProperties["width"];
  divPosition4?: CSSProperties["position"];
  divFontSize4?: CSSProperties["fontSize"];
  divTextTransform4?: CSSProperties["textTransform"];
  divFontFamily4?: CSSProperties["fontFamily"];
  divColor4?: CSSProperties["color"];
  divTextAlign4?: CSSProperties["textAlign"];
  divAlignSelf4?: CSSProperties["alignSelf"];
  divBorderRadius4?: CSSProperties["borderRadius"];
  divBackgroundColor4?: CSSProperties["backgroundColor"];
  divOverflow4?: CSSProperties["overflow"];
  divFlexDirection4?: CSSProperties["flexDirection"];
  divPadding4?: CSSProperties["padding"];
  divGap4?: CSSProperties["gap"];
  navigationOptionsAlignSelf5?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius6?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor6?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay6?: CSSProperties["display"];
  navigationOptionsFlexDirection6?: CSSProperties["flexDirection"];
  navigationOptionsPadding6?: CSSProperties["padding"];
  navigationOptionsGap6?: CSSProperties["gap"];
  navigationOptionsHeight5?: CSSProperties["height"];
  navigationOptionsWidth5?: CSSProperties["width"];
  navigationOptionsPosition5?: CSSProperties["position"];
  iconHeight5?: CSSProperties["height"];
  iconWidth4?: CSSProperties["width"];
  iconOverflow5?: CSSProperties["overflow"];
  iconFontSize5?: CSSProperties["fontSize"];
  iconTextTransform5?: CSSProperties["textTransform"];
  iconFontFamily5?: CSSProperties["fontFamily"];
  iconColor5?: CSSProperties["color"];
  iconTextAlign5?: CSSProperties["textAlign"];
  divWidth5?: CSSProperties["width"];
  divPosition5?: CSSProperties["position"];
  divFontSize5?: CSSProperties["fontSize"];
  divTextTransform5?: CSSProperties["textTransform"];
  divFontFamily5?: CSSProperties["fontFamily"];
  divColor5?: CSSProperties["color"];
  divTextAlign5?: CSSProperties["textAlign"];
  divAlignSelf5?: CSSProperties["alignSelf"];
  divBorderRadius5?: CSSProperties["borderRadius"];
  divBackgroundColor5?: CSSProperties["backgroundColor"];
  divOverflow5?: CSSProperties["overflow"];
  divFlexDirection5?: CSSProperties["flexDirection"];
  divPadding5?: CSSProperties["padding"];
  divGap5?: CSSProperties["gap"];
  navigationOptionsAlignSelf6?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius7?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor7?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay7?: CSSProperties["display"];
  navigationOptionsFlexDirection7?: CSSProperties["flexDirection"];
  navigationOptionsPadding7?: CSSProperties["padding"];
  navigationOptionsGap7?: CSSProperties["gap"];
  navigationOptionsHeight6?: CSSProperties["height"];
  navigationOptionsWidth6?: CSSProperties["width"];
  navigationOptionsPosition6?: CSSProperties["position"];
  iconHeight6?: CSSProperties["height"];
  iconWidth5?: CSSProperties["width"];
  iconOverflow6?: CSSProperties["overflow"];
  iconFontSize6?: CSSProperties["fontSize"];
  iconTextTransform6?: CSSProperties["textTransform"];
  iconFontFamily6?: CSSProperties["fontFamily"];
  iconColor6?: CSSProperties["color"];
  iconTextAlign6?: CSSProperties["textAlign"];
  divWidth6?: CSSProperties["width"];
  divPosition6?: CSSProperties["position"];
  divFontSize6?: CSSProperties["fontSize"];
  divTextTransform6?: CSSProperties["textTransform"];
  divFontFamily6?: CSSProperties["fontFamily"];
  divColor6?: CSSProperties["color"];
  divTextAlign6?: CSSProperties["textAlign"];
  divAlignSelf6?: CSSProperties["alignSelf"];
  divBorderRadius6?: CSSProperties["borderRadius"];
  divBackgroundColor6?: CSSProperties["backgroundColor"];
  divOverflow6?: CSSProperties["overflow"];
  divFlexDirection6?: CSSProperties["flexDirection"];
  divPadding6?: CSSProperties["padding"];
  divGap6?: CSSProperties["gap"];
  navigationOptionsAlignSelf7?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius8?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor8?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay8?: CSSProperties["display"];
  navigationOptionsFlexDirection8?: CSSProperties["flexDirection"];
  navigationOptionsPadding8?: CSSProperties["padding"];
  navigationOptionsGap8?: CSSProperties["gap"];
  navigationOptionsHeight7?: CSSProperties["height"];
  navigationOptionsWidth7?: CSSProperties["width"];
  navigationOptionsPosition7?: CSSProperties["position"];
  iconHeight7?: CSSProperties["height"];
  iconWidth6?: CSSProperties["width"];
  iconOverflow7?: CSSProperties["overflow"];
  iconFontSize7?: CSSProperties["fontSize"];
  iconTextTransform7?: CSSProperties["textTransform"];
  iconFontFamily7?: CSSProperties["fontFamily"];
  iconColor7?: CSSProperties["color"];
  iconTextAlign7?: CSSProperties["textAlign"];
  divWidth7?: CSSProperties["width"];
  divPosition7?: CSSProperties["position"];
  divFontSize7?: CSSProperties["fontSize"];
  divTextTransform7?: CSSProperties["textTransform"];
  divFontFamily7?: CSSProperties["fontFamily"];
  divColor7?: CSSProperties["color"];
  divTextAlign7?: CSSProperties["textAlign"];
  divAlignSelf7?: CSSProperties["alignSelf"];
  divBorderRadius7?: CSSProperties["borderRadius"];
  divBackgroundColor7?: CSSProperties["backgroundColor"];
  divOverflow7?: CSSProperties["overflow"];
  divFlexDirection7?: CSSProperties["flexDirection"];
  divPadding7?: CSSProperties["padding"];
  divGap7?: CSSProperties["gap"];
  navigationOptionsAlignSelf8?: CSSProperties["alignSelf"];
  navigationOptionsBorderRadius9?: CSSProperties["borderRadius"];
  navigationOptionsBackgroundColor9?: CSSProperties["backgroundColor"];
  navigationOptionsDisplay9?: CSSProperties["display"];
  navigationOptionsFlexDirection9?: CSSProperties["flexDirection"];
  navigationOptionsPadding9?: CSSProperties["padding"];
  navigationOptionsGap9?: CSSProperties["gap"];
  navigationOptionsHeight8?: CSSProperties["height"];
  navigationOptionsWidth8?: CSSProperties["width"];
  navigationOptionsPosition8?: CSSProperties["position"];
  iconHeight8?: CSSProperties["height"];
  iconWidth7?: CSSProperties["width"];
  iconOverflow8?: CSSProperties["overflow"];
  iconFontSize8?: CSSProperties["fontSize"];
  iconTextTransform8?: CSSProperties["textTransform"];
  iconFontFamily8?: CSSProperties["fontFamily"];
  iconColor8?: CSSProperties["color"];
  iconTextAlign8?: CSSProperties["textAlign"];
  divWidth8?: CSSProperties["width"];
  divPosition8?: CSSProperties["position"];
  divFontSize8?: CSSProperties["fontSize"];
  divTextTransform8?: CSSProperties["textTransform"];
  divFontFamily8?: CSSProperties["fontFamily"];
  divColor8?: CSSProperties["color"];
  divTextAlign8?: CSSProperties["textAlign"];
  divAlignSelf8?: CSSProperties["alignSelf"];
  divBorderRadius8?: CSSProperties["borderRadius"];
  divBackgroundColor8?: CSSProperties["backgroundColor"];
  divOverflow8?: CSSProperties["overflow"];
  divFlexDirection8?: CSSProperties["flexDirection"];
  divPadding8?: CSSProperties["padding"];
  divGap8?: CSSProperties["gap"];
};

const Component3: FunctionComponent<Component3Type> = ({
  className = "",
  propFlex,
  propWidth,
  propMargin,
  propPosition,
  propTop,
  propLeft,
  navigationOptionsBorderRadius,
  navigationOptionsBackgroundColor,
  navigationOptionsOverflow,
  navigationOptionsDisplay,
  navigationOptionsFlexDirection,
  navigationOptionsPadding,
  navigationOptionsGap,
  navigationOptionsAlignSelf,
  navigationOptionsBorderRadius1,
  navigationOptionsBackgroundColor1,
  navigationOptionsDisplay1,
  navigationOptionsFlexDirection1,
  navigationOptionsPadding1,
  navigationOptionsGap1,
  navigationOptionsHeight,
  navigationOptionsWidth,
  navigationOptionsPosition,
  showIcon,
  iconHeight,
  iconOverflow,
  iconFontSize,
  iconTextTransform,
  iconFontFamily,
  iconColor,
  iconTextAlign,
  prop1,
  div,
  divWidth,
  divPosition,
  divFontSize,
  divTextTransform,
  divFontFamily,
  divColor,
  divTextAlign,
  divAlignSelf,
  divBorderRadius,
  divBackgroundColor,
  divOverflow,
  divFlexDirection,
  divPadding,
  divGap,
  navigationOptionsAlignSelf1,
  navigationOptionsBorderRadius2,
  navigationOptionsBackgroundColor2,
  navigationOptionsDisplay2,
  navigationOptionsFlexDirection2,
  navigationOptionsPadding2,
  navigationOptionsGap2,
  navigationOptionsHeight1,
  navigationOptionsWidth1,
  navigationOptionsPosition1,
  iconVisible,
  iconHeight1,
  iconWidth,
  iconOverflow1,
  iconFontSize1,
  iconTextTransform1,
  iconFontFamily1,
  iconColor1,
  iconTextAlign1,
  prop3,
  div1,
  divWidth1,
  divPosition1,
  divFontSize1,
  divTextTransform1,
  divFontFamily1,
  divColor1,
  divTextAlign1,
  divAlignSelf1,
  divBorderRadius1,
  divBackgroundColor1,
  divOverflow1,
  divFlexDirection1,
  divPadding1,
  divGap1,
  navigationOptionsAlignSelf2,
  navigationOptionsBorderRadius3,
  navigationOptionsBackgroundColor3,
  navigationOptionsDisplay3,
  navigationOptionsFlexDirection3,
  navigationOptionsPadding3,
  navigationOptionsGap3,
  navigationOptionsHeight2,
  navigationOptionsWidth2,
  navigationOptionsPosition2,
  iconVisible1,
  iconHeight2,
  iconWidth1,
  iconOverflow2,
  iconFontSize2,
  iconTextTransform2,
  iconFontFamily2,
  iconColor2,
  iconTextAlign2,
  prop5,
  div2,
  divWidth2,
  divPosition2,
  divFontSize2,
  divTextTransform2,
  divFontFamily2,
  divColor2,
  divTextAlign2,
  divAlignSelf2,
  divBorderRadius2,
  divBackgroundColor2,
  divOverflow2,
  divFlexDirection2,
  divPadding2,
  divGap2,
  navigationOptionsAlignSelf3,
  navigationOptionsBorderRadius4,
  navigationOptionsBackgroundColor4,
  navigationOptionsDisplay4,
  navigationOptionsFlexDirection4,
  navigationOptionsPadding4,
  navigationOptionsGap4,
  navigationOptionsHeight3,
  navigationOptionsWidth3,
  navigationOptionsPosition3,
  iconVisible2,
  iconHeight3,
  iconWidth2,
  iconOverflow3,
  iconFontSize3,
  iconTextTransform3,
  iconFontFamily3,
  iconColor3,
  iconTextAlign3,
  prop7,
  div3,
  divWidth3,
  divPosition3,
  divFontSize3,
  divTextTransform3,
  divFontFamily3,
  divColor3,
  divTextAlign3,
  divAlignSelf3,
  divBorderRadius3,
  divBackgroundColor3,
  divOverflow3,
  divFlexDirection3,
  divPadding3,
  divGap3,
  navigationOptionsAlignSelf4,
  navigationOptionsBorderRadius5,
  navigationOptionsBackgroundColor5,
  navigationOptionsDisplay5,
  navigationOptionsFlexDirection5,
  navigationOptionsPadding5,
  navigationOptionsGap5,
  navigationOptionsHeight4,
  navigationOptionsWidth4,
  navigationOptionsPosition4,
  iconVisible3,
  iconHeight4,
  iconWidth3,
  iconOverflow4,
  iconFontSize4,
  iconTextTransform4,
  iconFontFamily4,
  iconColor4,
  iconTextAlign4,
  prop9,
  div4,
  divWidth4,
  divPosition4,
  divFontSize4,
  divTextTransform4,
  divFontFamily4,
  divColor4,
  divTextAlign4,
  divAlignSelf4,
  divBorderRadius4,
  divBackgroundColor4,
  divOverflow4,
  divFlexDirection4,
  divPadding4,
  divGap4,
  navigationOptionsAlignSelf5,
  navigationOptionsBorderRadius6,
  navigationOptionsBackgroundColor6,
  navigationOptionsDisplay6,
  navigationOptionsFlexDirection6,
  navigationOptionsPadding6,
  navigationOptionsGap6,
  navigationOptionsHeight5,
  navigationOptionsWidth5,
  navigationOptionsPosition5,
  iconVisible4,
  iconHeight5,
  iconWidth4,
  iconOverflow5,
  iconFontSize5,
  iconTextTransform5,
  iconFontFamily5,
  iconColor5,
  iconTextAlign5,
  prop11,
  div5,
  divWidth5,
  divPosition5,
  divFontSize5,
  divTextTransform5,
  divFontFamily5,
  divColor5,
  divTextAlign5,
  divAlignSelf5,
  divBorderRadius5,
  divBackgroundColor5,
  divOverflow5,
  divFlexDirection5,
  divPadding5,
  divGap5,
  navigationOptionsAlignSelf6,
  navigationOptionsBorderRadius7,
  navigationOptionsBackgroundColor7,
  navigationOptionsDisplay7,
  navigationOptionsFlexDirection7,
  navigationOptionsPadding7,
  navigationOptionsGap7,
  navigationOptionsHeight6,
  navigationOptionsWidth6,
  navigationOptionsPosition6,
  iconVisible5,
  iconHeight6,
  iconWidth5,
  iconOverflow6,
  iconFontSize6,
  iconTextTransform6,
  iconFontFamily6,
  iconColor6,
  iconTextAlign6,
  prop13,
  div6,
  divWidth6,
  divPosition6,
  divFontSize6,
  divTextTransform6,
  divFontFamily6,
  divColor6,
  divTextAlign6,
  divAlignSelf6,
  divBorderRadius6,
  divBackgroundColor6,
  divOverflow6,
  divFlexDirection6,
  divPadding6,
  divGap6,
  navigationOptionsAlignSelf7,
  navigationOptionsBorderRadius8,
  navigationOptionsBackgroundColor8,
  navigationOptionsDisplay8,
  navigationOptionsFlexDirection8,
  navigationOptionsPadding8,
  navigationOptionsGap8,
  navigationOptionsHeight7,
  navigationOptionsWidth7,
  navigationOptionsPosition7,
  iconVisible6,
  iconHeight7,
  iconWidth6,
  iconOverflow7,
  iconFontSize7,
  iconTextTransform7,
  iconFontFamily7,
  iconColor7,
  iconTextAlign7,
  prop15,
  div7,
  divWidth7,
  divPosition7,
  divFontSize7,
  divTextTransform7,
  divFontFamily7,
  divColor7,
  divTextAlign7,
  divAlignSelf7,
  divBorderRadius7,
  divBackgroundColor7,
  divOverflow7,
  divFlexDirection7,
  divPadding7,
  divGap7,
  navigationOptionsAlignSelf8,
  navigationOptionsBorderRadius9,
  navigationOptionsBackgroundColor9,
  navigationOptionsDisplay9,
  navigationOptionsFlexDirection9,
  navigationOptionsPadding9,
  navigationOptionsGap9,
  navigationOptionsHeight8,
  navigationOptionsWidth8,
  navigationOptionsPosition8,
  iconVisible7,
  iconHeight8,
  iconWidth7,
  iconOverflow8,
  iconFontSize8,
  iconTextTransform8,
  iconFontFamily8,
  iconColor8,
  iconTextAlign8,
  prop17,
  div8,
  divWidth8,
  divPosition8,
  divFontSize8,
  divTextTransform8,
  divFontFamily8,
  divColor8,
  divTextAlign8,
  divAlignSelf8,
  divBorderRadius8,
  divBackgroundColor8,
  divOverflow8,
  divFlexDirection8,
  divPadding8,
  divGap8,
}) => {
  const component39Style: CSSProperties = useMemo(() => {
    return {
      flex: propFlex,
      width: propWidth,
      margin: propMargin,
      position: propPosition,
      top: propTop,
      left: propLeft,
    };
  }, [propFlex, propWidth, propMargin, propPosition, propTop, propLeft]);

  const navigationOptionsStyle: CSSProperties = useMemo(() => {
    return {
      borderRadius: navigationOptionsBorderRadius,
      backgroundColor: navigationOptionsBackgroundColor,
      overflow: navigationOptionsOverflow,
      display: navigationOptionsDisplay,
      flexDirection: navigationOptionsFlexDirection,
      padding: navigationOptionsPadding,
      gap: navigationOptionsGap,
    };
  }, [
    navigationOptionsBorderRadius,
    navigationOptionsBackgroundColor,
    navigationOptionsOverflow,
    navigationOptionsDisplay,
    navigationOptionsFlexDirection,
    navigationOptionsPadding,
    navigationOptionsGap,
  ]);

  const navigationOptions1Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf,
      borderRadius: navigationOptionsBorderRadius1,
      backgroundColor: navigationOptionsBackgroundColor1,
      display: navigationOptionsDisplay1,
      flexDirection: navigationOptionsFlexDirection1,
      padding: navigationOptionsPadding1,
      gap: navigationOptionsGap1,
      height: navigationOptionsHeight,
      width: navigationOptionsWidth,
      position: navigationOptionsPosition,
    };
  }, [
    navigationOptionsAlignSelf,
    navigationOptionsBorderRadius1,
    navigationOptionsBackgroundColor1,
    navigationOptionsDisplay1,
    navigationOptionsFlexDirection1,
    navigationOptionsPadding1,
    navigationOptionsGap1,
    navigationOptionsHeight,
    navigationOptionsWidth,
    navigationOptionsPosition,
  ]);

  const icon1Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight,
      overflow: iconOverflow,
      fontSize: iconFontSize,
      textTransform: iconTextTransform,
      fontFamily: iconFontFamily,
      color: iconColor,
      textAlign: iconTextAlign,
    };
  }, [
    iconHeight,
    iconOverflow,
    iconFontSize,
    iconTextTransform,
    iconFontFamily,
    iconColor,
    iconTextAlign,
  ]);

  const div7Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth,
      position: divPosition,
      fontSize: divFontSize,
      textTransform: divTextTransform,
      fontFamily: divFontFamily,
      color: divColor,
      textAlign: divTextAlign,
      alignSelf: divAlignSelf,
      borderRadius: divBorderRadius,
      backgroundColor: divBackgroundColor,
      overflow: divOverflow,
      flexDirection: divFlexDirection,
      padding: divPadding,
      gap: divGap,
    };
  }, [
    divWidth,
    divPosition,
    divFontSize,
    divTextTransform,
    divFontFamily,
    divColor,
    divTextAlign,
    divAlignSelf,
    divBorderRadius,
    divBackgroundColor,
    divOverflow,
    divFlexDirection,
    divPadding,
    divGap,
  ]);

  const navigationOptions2Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf1,
      borderRadius: navigationOptionsBorderRadius2,
      backgroundColor: navigationOptionsBackgroundColor2,
      display: navigationOptionsDisplay2,
      flexDirection: navigationOptionsFlexDirection2,
      padding: navigationOptionsPadding2,
      gap: navigationOptionsGap2,
      height: navigationOptionsHeight1,
      width: navigationOptionsWidth1,
      position: navigationOptionsPosition1,
    };
  }, [
    navigationOptionsAlignSelf1,
    navigationOptionsBorderRadius2,
    navigationOptionsBackgroundColor2,
    navigationOptionsDisplay2,
    navigationOptionsFlexDirection2,
    navigationOptionsPadding2,
    navigationOptionsGap2,
    navigationOptionsHeight1,
    navigationOptionsWidth1,
    navigationOptionsPosition1,
  ]);

  const icon2Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight1,
      width: iconWidth,
      overflow: iconOverflow1,
      fontSize: iconFontSize1,
      textTransform: iconTextTransform1,
      fontFamily: iconFontFamily1,
      color: iconColor1,
      textAlign: iconTextAlign1,
    };
  }, [
    iconHeight1,
    iconWidth,
    iconOverflow1,
    iconFontSize1,
    iconTextTransform1,
    iconFontFamily1,
    iconColor1,
    iconTextAlign1,
  ]);

  const div8Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth1,
      position: divPosition1,
      fontSize: divFontSize1,
      textTransform: divTextTransform1,
      fontFamily: divFontFamily1,
      color: divColor1,
      textAlign: divTextAlign1,
      alignSelf: divAlignSelf1,
      borderRadius: divBorderRadius1,
      backgroundColor: divBackgroundColor1,
      overflow: divOverflow1,
      flexDirection: divFlexDirection1,
      padding: divPadding1,
      gap: divGap1,
    };
  }, [
    divWidth1,
    divPosition1,
    divFontSize1,
    divTextTransform1,
    divFontFamily1,
    divColor1,
    divTextAlign1,
    divAlignSelf1,
    divBorderRadius1,
    divBackgroundColor1,
    divOverflow1,
    divFlexDirection1,
    divPadding1,
    divGap1,
  ]);

  const navigationOptions3Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf2,
      borderRadius: navigationOptionsBorderRadius3,
      backgroundColor: navigationOptionsBackgroundColor3,
      display: navigationOptionsDisplay3,
      flexDirection: navigationOptionsFlexDirection3,
      padding: navigationOptionsPadding3,
      gap: navigationOptionsGap3,
      height: navigationOptionsHeight2,
      width: navigationOptionsWidth2,
      position: navigationOptionsPosition2,
    };
  }, [
    navigationOptionsAlignSelf2,
    navigationOptionsBorderRadius3,
    navigationOptionsBackgroundColor3,
    navigationOptionsDisplay3,
    navigationOptionsFlexDirection3,
    navigationOptionsPadding3,
    navigationOptionsGap3,
    navigationOptionsHeight2,
    navigationOptionsWidth2,
    navigationOptionsPosition2,
  ]);

  const icon3Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight2,
      width: iconWidth1,
      overflow: iconOverflow2,
      fontSize: iconFontSize2,
      textTransform: iconTextTransform2,
      fontFamily: iconFontFamily2,
      color: iconColor2,
      textAlign: iconTextAlign2,
    };
  }, [
    iconHeight2,
    iconWidth1,
    iconOverflow2,
    iconFontSize2,
    iconTextTransform2,
    iconFontFamily2,
    iconColor2,
    iconTextAlign2,
  ]);

  const div9Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth2,
      position: divPosition2,
      fontSize: divFontSize2,
      textTransform: divTextTransform2,
      fontFamily: divFontFamily2,
      color: divColor2,
      textAlign: divTextAlign2,
      alignSelf: divAlignSelf2,
      borderRadius: divBorderRadius2,
      backgroundColor: divBackgroundColor2,
      overflow: divOverflow2,
      flexDirection: divFlexDirection2,
      padding: divPadding2,
      gap: divGap2,
    };
  }, [
    divWidth2,
    divPosition2,
    divFontSize2,
    divTextTransform2,
    divFontFamily2,
    divColor2,
    divTextAlign2,
    divAlignSelf2,
    divBorderRadius2,
    divBackgroundColor2,
    divOverflow2,
    divFlexDirection2,
    divPadding2,
    divGap2,
  ]);

  const navigationOptions4Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf3,
      borderRadius: navigationOptionsBorderRadius4,
      backgroundColor: navigationOptionsBackgroundColor4,
      display: navigationOptionsDisplay4,
      flexDirection: navigationOptionsFlexDirection4,
      padding: navigationOptionsPadding4,
      gap: navigationOptionsGap4,
      height: navigationOptionsHeight3,
      width: navigationOptionsWidth3,
      position: navigationOptionsPosition3,
    };
  }, [
    navigationOptionsAlignSelf3,
    navigationOptionsBorderRadius4,
    navigationOptionsBackgroundColor4,
    navigationOptionsDisplay4,
    navigationOptionsFlexDirection4,
    navigationOptionsPadding4,
    navigationOptionsGap4,
    navigationOptionsHeight3,
    navigationOptionsWidth3,
    navigationOptionsPosition3,
  ]);

  const icon4Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight3,
      width: iconWidth2,
      overflow: iconOverflow3,
      fontSize: iconFontSize3,
      textTransform: iconTextTransform3,
      fontFamily: iconFontFamily3,
      color: iconColor3,
      textAlign: iconTextAlign3,
    };
  }, [
    iconHeight3,
    iconWidth2,
    iconOverflow3,
    iconFontSize3,
    iconTextTransform3,
    iconFontFamily3,
    iconColor3,
    iconTextAlign3,
  ]);

  const div10Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth3,
      position: divPosition3,
      fontSize: divFontSize3,
      textTransform: divTextTransform3,
      fontFamily: divFontFamily3,
      color: divColor3,
      textAlign: divTextAlign3,
      alignSelf: divAlignSelf3,
      borderRadius: divBorderRadius3,
      backgroundColor: divBackgroundColor3,
      overflow: divOverflow3,
      flexDirection: divFlexDirection3,
      padding: divPadding3,
      gap: divGap3,
    };
  }, [
    divWidth3,
    divPosition3,
    divFontSize3,
    divTextTransform3,
    divFontFamily3,
    divColor3,
    divTextAlign3,
    divAlignSelf3,
    divBorderRadius3,
    divBackgroundColor3,
    divOverflow3,
    divFlexDirection3,
    divPadding3,
    divGap3,
  ]);

  const navigationOptions5Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf4,
      borderRadius: navigationOptionsBorderRadius5,
      backgroundColor: navigationOptionsBackgroundColor5,
      display: navigationOptionsDisplay5,
      flexDirection: navigationOptionsFlexDirection5,
      padding: navigationOptionsPadding5,
      gap: navigationOptionsGap5,
      height: navigationOptionsHeight4,
      width: navigationOptionsWidth4,
      position: navigationOptionsPosition4,
    };
  }, [
    navigationOptionsAlignSelf4,
    navigationOptionsBorderRadius5,
    navigationOptionsBackgroundColor5,
    navigationOptionsDisplay5,
    navigationOptionsFlexDirection5,
    navigationOptionsPadding5,
    navigationOptionsGap5,
    navigationOptionsHeight4,
    navigationOptionsWidth4,
    navigationOptionsPosition4,
  ]);

  const icon5Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight4,
      width: iconWidth3,
      overflow: iconOverflow4,
      fontSize: iconFontSize4,
      textTransform: iconTextTransform4,
      fontFamily: iconFontFamily4,
      color: iconColor4,
      textAlign: iconTextAlign4,
    };
  }, [
    iconHeight4,
    iconWidth3,
    iconOverflow4,
    iconFontSize4,
    iconTextTransform4,
    iconFontFamily4,
    iconColor4,
    iconTextAlign4,
  ]);

  const div11Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth4,
      position: divPosition4,
      fontSize: divFontSize4,
      textTransform: divTextTransform4,
      fontFamily: divFontFamily4,
      color: divColor4,
      textAlign: divTextAlign4,
      alignSelf: divAlignSelf4,
      borderRadius: divBorderRadius4,
      backgroundColor: divBackgroundColor4,
      overflow: divOverflow4,
      flexDirection: divFlexDirection4,
      padding: divPadding4,
      gap: divGap4,
    };
  }, [
    divWidth4,
    divPosition4,
    divFontSize4,
    divTextTransform4,
    divFontFamily4,
    divColor4,
    divTextAlign4,
    divAlignSelf4,
    divBorderRadius4,
    divBackgroundColor4,
    divOverflow4,
    divFlexDirection4,
    divPadding4,
    divGap4,
  ]);

  const navigationOptions6Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf5,
      borderRadius: navigationOptionsBorderRadius6,
      backgroundColor: navigationOptionsBackgroundColor6,
      display: navigationOptionsDisplay6,
      flexDirection: navigationOptionsFlexDirection6,
      padding: navigationOptionsPadding6,
      gap: navigationOptionsGap6,
      height: navigationOptionsHeight5,
      width: navigationOptionsWidth5,
      position: navigationOptionsPosition5,
    };
  }, [
    navigationOptionsAlignSelf5,
    navigationOptionsBorderRadius6,
    navigationOptionsBackgroundColor6,
    navigationOptionsDisplay6,
    navigationOptionsFlexDirection6,
    navigationOptionsPadding6,
    navigationOptionsGap6,
    navigationOptionsHeight5,
    navigationOptionsWidth5,
    navigationOptionsPosition5,
  ]);

  const icon6Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight5,
      width: iconWidth4,
      overflow: iconOverflow5,
      fontSize: iconFontSize5,
      textTransform: iconTextTransform5,
      fontFamily: iconFontFamily5,
      color: iconColor5,
      textAlign: iconTextAlign5,
    };
  }, [
    iconHeight5,
    iconWidth4,
    iconOverflow5,
    iconFontSize5,
    iconTextTransform5,
    iconFontFamily5,
    iconColor5,
    iconTextAlign5,
  ]);

  const div12Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth5,
      position: divPosition5,
      fontSize: divFontSize5,
      textTransform: divTextTransform5,
      fontFamily: divFontFamily5,
      color: divColor5,
      textAlign: divTextAlign5,
      alignSelf: divAlignSelf5,
      borderRadius: divBorderRadius5,
      backgroundColor: divBackgroundColor5,
      overflow: divOverflow5,
      flexDirection: divFlexDirection5,
      padding: divPadding5,
      gap: divGap5,
    };
  }, [
    divWidth5,
    divPosition5,
    divFontSize5,
    divTextTransform5,
    divFontFamily5,
    divColor5,
    divTextAlign5,
    divAlignSelf5,
    divBorderRadius5,
    divBackgroundColor5,
    divOverflow5,
    divFlexDirection5,
    divPadding5,
    divGap5,
  ]);

  const navigationOptions7Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf6,
      borderRadius: navigationOptionsBorderRadius7,
      backgroundColor: navigationOptionsBackgroundColor7,
      display: navigationOptionsDisplay7,
      flexDirection: navigationOptionsFlexDirection7,
      padding: navigationOptionsPadding7,
      gap: navigationOptionsGap7,
      height: navigationOptionsHeight6,
      width: navigationOptionsWidth6,
      position: navigationOptionsPosition6,
    };
  }, [
    navigationOptionsAlignSelf6,
    navigationOptionsBorderRadius7,
    navigationOptionsBackgroundColor7,
    navigationOptionsDisplay7,
    navigationOptionsFlexDirection7,
    navigationOptionsPadding7,
    navigationOptionsGap7,
    navigationOptionsHeight6,
    navigationOptionsWidth6,
    navigationOptionsPosition6,
  ]);

  const icon7Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight6,
      width: iconWidth5,
      overflow: iconOverflow6,
      fontSize: iconFontSize6,
      textTransform: iconTextTransform6,
      fontFamily: iconFontFamily6,
      color: iconColor6,
      textAlign: iconTextAlign6,
    };
  }, [
    iconHeight6,
    iconWidth5,
    iconOverflow6,
    iconFontSize6,
    iconTextTransform6,
    iconFontFamily6,
    iconColor6,
    iconTextAlign6,
  ]);

  const div13Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth6,
      position: divPosition6,
      fontSize: divFontSize6,
      textTransform: divTextTransform6,
      fontFamily: divFontFamily6,
      color: divColor6,
      textAlign: divTextAlign6,
      alignSelf: divAlignSelf6,
      borderRadius: divBorderRadius6,
      backgroundColor: divBackgroundColor6,
      overflow: divOverflow6,
      flexDirection: divFlexDirection6,
      padding: divPadding6,
      gap: divGap6,
    };
  }, [
    divWidth6,
    divPosition6,
    divFontSize6,
    divTextTransform6,
    divFontFamily6,
    divColor6,
    divTextAlign6,
    divAlignSelf6,
    divBorderRadius6,
    divBackgroundColor6,
    divOverflow6,
    divFlexDirection6,
    divPadding6,
    divGap6,
  ]);

  const navigationOptions8Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf7,
      borderRadius: navigationOptionsBorderRadius8,
      backgroundColor: navigationOptionsBackgroundColor8,
      display: navigationOptionsDisplay8,
      flexDirection: navigationOptionsFlexDirection8,
      padding: navigationOptionsPadding8,
      gap: navigationOptionsGap8,
      height: navigationOptionsHeight7,
      width: navigationOptionsWidth7,
      position: navigationOptionsPosition7,
    };
  }, [
    navigationOptionsAlignSelf7,
    navigationOptionsBorderRadius8,
    navigationOptionsBackgroundColor8,
    navigationOptionsDisplay8,
    navigationOptionsFlexDirection8,
    navigationOptionsPadding8,
    navigationOptionsGap8,
    navigationOptionsHeight7,
    navigationOptionsWidth7,
    navigationOptionsPosition7,
  ]);

  const icon8Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight7,
      width: iconWidth6,
      overflow: iconOverflow7,
      fontSize: iconFontSize7,
      textTransform: iconTextTransform7,
      fontFamily: iconFontFamily7,
      color: iconColor7,
      textAlign: iconTextAlign7,
    };
  }, [
    iconHeight7,
    iconWidth6,
    iconOverflow7,
    iconFontSize7,
    iconTextTransform7,
    iconFontFamily7,
    iconColor7,
    iconTextAlign7,
  ]);

  const div14Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth7,
      position: divPosition7,
      fontSize: divFontSize7,
      textTransform: divTextTransform7,
      fontFamily: divFontFamily7,
      color: divColor7,
      textAlign: divTextAlign7,
      alignSelf: divAlignSelf7,
      borderRadius: divBorderRadius7,
      backgroundColor: divBackgroundColor7,
      overflow: divOverflow7,
      flexDirection: divFlexDirection7,
      padding: divPadding7,
      gap: divGap7,
    };
  }, [
    divWidth7,
    divPosition7,
    divFontSize7,
    divTextTransform7,
    divFontFamily7,
    divColor7,
    divTextAlign7,
    divAlignSelf7,
    divBorderRadius7,
    divBackgroundColor7,
    divOverflow7,
    divFlexDirection7,
    divPadding7,
    divGap7,
  ]);

  const navigationOptions9Style: CSSProperties = useMemo(() => {
    return {
      alignSelf: navigationOptionsAlignSelf8,
      borderRadius: navigationOptionsBorderRadius9,
      backgroundColor: navigationOptionsBackgroundColor9,
      display: navigationOptionsDisplay9,
      flexDirection: navigationOptionsFlexDirection9,
      padding: navigationOptionsPadding9,
      gap: navigationOptionsGap9,
      height: navigationOptionsHeight8,
      width: navigationOptionsWidth8,
      position: navigationOptionsPosition8,
    };
  }, [
    navigationOptionsAlignSelf8,
    navigationOptionsBorderRadius9,
    navigationOptionsBackgroundColor9,
    navigationOptionsDisplay9,
    navigationOptionsFlexDirection9,
    navigationOptionsPadding9,
    navigationOptionsGap9,
    navigationOptionsHeight8,
    navigationOptionsWidth8,
    navigationOptionsPosition8,
  ]);

  const icon9Style: CSSProperties = useMemo(() => {
    return {
      height: iconHeight8,
      width: iconWidth7,
      overflow: iconOverflow8,
      fontSize: iconFontSize8,
      textTransform: iconTextTransform8,
      fontFamily: iconFontFamily8,
      color: iconColor8,
      textAlign: iconTextAlign8,
    };
  }, [
    iconHeight8,
    iconWidth7,
    iconOverflow8,
    iconFontSize8,
    iconTextTransform8,
    iconFontFamily8,
    iconColor8,
    iconTextAlign8,
  ]);

  const div15Style: CSSProperties = useMemo(() => {
    return {
      width: divWidth8,
      position: divPosition8,
      fontSize: divFontSize8,
      textTransform: divTextTransform8,
      fontFamily: divFontFamily8,
      color: divColor8,
      textAlign: divTextAlign8,
      alignSelf: divAlignSelf8,
      borderRadius: divBorderRadius8,
      backgroundColor: divBackgroundColor8,
      overflow: divOverflow8,
      flexDirection: divFlexDirection8,
      padding: divPadding8,
      gap: divGap8,
    };
  }, [
    divWidth8,
    divPosition8,
    divFontSize8,
    divTextTransform8,
    divFontFamily8,
    divColor8,
    divTextAlign8,
    divAlignSelf8,
    divBorderRadius8,
    divBackgroundColor8,
    divOverflow8,
    divFlexDirection8,
    divPadding8,
    divGap8,
  ]);

 return (
    <div
      className={`flex-1 flex flex-col items-start justify-start max-w-full z-[3] text-left text-base text-white font-inter ${className}`}
      style={component39Style}
    >
      <div className="self-stretch  rounded-19xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.09375rem] px-[2.625rem] gap-[1.875rem]">
        <img
          className="h-[2.1875rem] w-[2.1875rem] relative overflow-hidden shrink-0"
          loading="lazy"
          alt=""
          src="/.svg"
        />
        <div className="w-[4.5625rem] relative uppercase hidden">Профіль</div>
      </div>
      <div className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.09375rem] px-[2.625rem] gap-[1.875rem]">
        <img
          className="h-[2.1875rem] w-[2.1875rem] relative overflow-hidden shrink-0"
          loading="lazy"
          alt=""
          src="/-1.svg"
        />
        <div className="w-[3.75rem] relative uppercase hidden">Пошук</div>
      </div>
      <div className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.09375rem] px-[2.625rem] gap-[1.875rem]">
        <img
          className="h-[2.1875rem] w-[2.1875rem] relative overflow-hidden shrink-0"
          loading="lazy"
          alt=""
          src="/chat.svg"
        />
        <div className="w-[3.75rem] relative uppercase hidden">Чат</div>
      </div>
      
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start pt-[1.16875rem] px-[2.625rem] pb-[1.175rem] gap-[1.875rem]"
        style={navigationOptions1Style}
      >
        {showIcon && (
          <img
            className="h-[2.03125rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/-3.svg"
            style={icon1Style}
          />
        )}
        {!div && (
          <div className="w-[6.75rem] relative uppercase" style={div7Style}>
            {prop1}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.16875rem] px-[2.625rem] gap-[1.875rem]"
        style={navigationOptions2Style}
      >
        {iconVisible && (
          <img
            className="h-[2.0375rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/-4.svg"
            style={icon2Style}
          />
        )}
        {!div1 && (
          <div className="w-[5.4375rem] relative uppercase" style={div8Style}>
            {prop3}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.1125rem] px-[2.625rem] gap-[1.875rem]"
        style={navigationOptions3Style}
      >
        {iconVisible1 && (
          <img
            className="h-[2.15rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/-31.svg"
            style={icon3Style}
          />
        )}
        {!div2 && (
          <div className="w-[4.3125rem] relative uppercase" style={div9Style}>
            {prop5}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.29375rem] px-[2.625rem] gap-[1.875rem]"
        style={navigationOptions4Style}
      >
        {iconVisible2 && (
          <img
            className="h-[1.7875rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            alt=""
            src="/-5.svg"
            style={icon4Style}
          />
        )}
        {!div3 && (
          <div className="w-[4.4375rem] relative uppercase" style={div10Style}>
            {prop7}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.26875rem] px-[2.625rem] gap-[1.875rem]"
        style={navigationOptions5Style}
      >
        {iconVisible3 && (
          <img
            className="h-[1.8375rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/-6.svg"
            style={icon5Style}
          />
        )}
        {!div4 && (
          <div className="w-[6.875rem] relative uppercase" style={div11Style}>
            {prop9}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[0.9rem] px-[2.625rem] gap-[1.875rem]"
        style={navigationOptions6Style}
      >
        {iconVisible4 && (
          <img
            className="h-[2.575rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/-7.svg"
            style={icon6Style}
          />
        )}
        {!div5 && (
          <div className="w-[5.6875rem] relative uppercase" style={div12Style}>
            {prop11}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start py-[1.09375rem] px-[2.625rem] gap-[1.875rem] whitespace-nowrap"
        style={navigationOptions7Style}
      >
        {iconVisible5 && (
          <img
            className="h-[2.1875rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/-8.svg"
            style={icon7Style}
          />
        )}
        {!div6 && (
          <div className="w-[9.3125rem] relative uppercase" style={div13Style}>
            {prop13}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start pt-[1.19375rem] px-[2.625rem] pb-[1.2rem] gap-[1.875rem]"
        style={navigationOptions8Style}
      >
        {iconVisible6 && (
          <img
            className="h-[1.98125rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/-9.svg"
            style={icon8Style}
          />
        )}
        {!div7 && (
          <div className="w-[8.5rem] relative uppercase" style={div14Style}>
            {prop15}
          </div>
        )}
      </div>
      <div
        className="self-stretch rounded-20xl bg-darkslategray-300 overflow-hidden flex flex-row items-start justify-start pt-[1.1125rem] px-[2.625rem] pb-[1.11875rem] gap-[1.875rem] whitespace-nowrap z-[1]"
        style={navigationOptions9Style}
      >
        {iconVisible7 && (
          <img
            className="h-[2.14375rem] w-[2.1875rem] relative overflow-hidden shrink-0"
            loading="lazy"
            alt=""
            src="/--3.svg"
            style={icon9Style}
          />
        )}
        {!div8 && (
          <div className="w-[10.6875rem] relative uppercase" style={div15Style}>
            {prop17}
          </div>
        )}
      </div>
    </div>
  );

};

export default Component3;
