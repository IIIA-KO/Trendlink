import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import {useProfile} from "../hooks/useProfile";
import {useNavigate} from "react-router-dom";
import StatisticsBar from "../components/StatisticsBar";
import PDF from '../assets/Contract.pdf';

const ProfilePage: React.FC = () => {

    const { posts, loading, fetchPosts, hasNextPage, hasPreviousPage, afterCursor, beforeCursor } = useProfile();
    const navigate = useNavigate();

    if (loading) {
        navigate("/loading")
    }

    return (
        <div className="bg-background flex justify-start h-auto w-auto">
            <div className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-28">
                <Navbar />
            </div>
            <div className="w-5/6 h-auto">
                <div
                    className="flex flex-col gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar/>
                    <StatisticsBar/>
                    <div className="h-3.5 justify-start items-center gap-0.5 inline-flex relative left-[50vw]">
                        <div className="w-3.5 h-3.5 justify-center items-center flex">
                        <div className="w-3.5 h-3.5 relative">
                        </div>
                       </div>
                       <div className="text-[#7c7b7b] text-xs font-normal font-['Inter'] underline"><a href={PDF}>Зберегти в PDF</a></div>
                    </div>
                    <div className="h-full w-full py-4 px-12">
                        <div className="flex justify-center">
                         <div className="w-96 relative">
                         <div style={{textAlign: 'center', color: '#3C3C3C', fontSize: 14, fontFamily: 'Inter', fontWeight: '400', wordWrap: 'break-word'}}>Договір <br/>про проведення рекламної кампанії</div><br/><br/>
                         <div className="w-96 left-0"><span className="text-[#3c3c3c] text-sm font-bold font-['Inter']">1. Загальні положення<br/></span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter']"><br/>1.1. Цей Договір (далі – Договір) укладений між:<br/>Замовник: __________, в особі __________, що діє на підставі __________, з одного боку, і<br/>Виконавець: __________ (далі – Блогер), в особі __________, що діє на підставі __________, з іншого боку.<br/><br/>1.2. Предметом цього Договору є проведення Блогером рекламної кампанії в соціальних мережах (далі – Кампанія) на умовах, передбачених цим Договором.<br/></span></div><br/>
                         <div className="w-96 left-0"><span className="text-[#3c3c3c] text-sm font-bold font-['Inter']">2. Обов’язки сторін<br/></span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter']"><br/>2.1. Блогер зобов’язується:<br/><br/>       2.1.1. Підготувати та опублікувати рекламні матеріали у своєму акаунті соціальної мережі відповідно до узгодженого плану.<br/>       2.1.2. Забезпечити якість і відповідність рекламних матеріалів вимогам Замовника.<br/>       2.1.3. Дотримуватися термінів публікації рекламних матеріалів, встановлених цим Договором.<br/>       2.1.4. Не видаляти і не змінювати опубліковані рекламні матеріали без погодження із Замовником протягом 14 днів з моменту    публікації.<br/><br/>2.2. Замовник зобов’язується:<br/><br/>       2.2.1. Надати Блогеру всю необхідну інформацію та матеріали для проведення Кампанії.<br/>       2.2.2. Оплатити послуги Блогера відповідно до умов цього Договору.<br/>       2.2.3. Не втручатися у творчий процес створення рекламних матеріалів, за винятком випадків, передбачених цим Договором.<br/></span></div>
                         <div className="w-96 left-0"><span className="text-[#3c3c3c] text-sm font-bold font-['Inter']">3. Вартість послуг і порядок розрахунків<br/></span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter']"><br/>3.1. Вартість послуг Блогера за цим Договором становить __________ (__________) гривень/доларів США/інша валюта.<br/><br/>3.2. Оплата послуг здійснюється в наступному порядку:<br/><br/>       3.2.1. 100% від загальної суми авансовим платежем протягом 3 робочих днів з моменту підписання Договору.</span></div>
                         <div className="w-96 left-0"><span className="text-[#3c3c3c] text-sm font-bold font-['Inter']">4. Термін дії договору<br/></span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter']"><br/>4.1. Цей Договір набуває чинності з моменту його підписання сторонами і діє до повного виконання сторонами своїх зобов’язань.<br/><br/>4.2. Договір може бути продовжений за домовленістю сторін, оформленою в письмовій формі.</span></div>
                         <div className="w-96 left-0"><span className="text-[#3c3c3c] text-sm font-bold font-['Inter']">5. Відповідальність сторін<br/></span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter']"><br/>5.1. За невиконання або неналежне виконання зобов’язань за цим Договором сторони несуть відповідальність відповідно до чинного законодавства України.<br/><br/>5.2. Блогер несе відповідальність за якість і своєчасність виконання своїх зобов’язань.<br/><br/>5.3. Замовник несе відповідальність за своєчасне надання матеріалів і оплату послуг.</span></div>
                         <div className="w-96 left-0"><span className="text-[#3c3c3c] text-sm font-bold font-['Inter']">6. Інші умови<br/></span><span className="text-[#3c3c3c] text-sm font-normal font-['Inter']"><br/>6.1. Усі зміни та доповнення до цього Договору дійсні лише в разі, якщо вони оформлені в письмовій формі та підписані обома сторонами.<br/><br/>6.2. У разі виникнення спорів, сторони зобов’язуються вирішувати їх шляхом переговорів. У разі неможливості досягнення угоди, спір підлягає розгляду в суді за місцем знаходження Замовника</span></div>
                    </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    );
};

export default ProfilePage;