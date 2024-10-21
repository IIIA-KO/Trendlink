import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import sharp from "../assets/Sharp-img.svg"
import TermsOfCooperationForm from "../components/Forms/TermsOfCooperationForm";

const TermsOfCooperationPage: React.FC = () => {
    return (
        <div className="bg-background flex justify-start h-auto w-auto">
            <div
                className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-28">
                <Navbar/>
            </div>
            <div className="w-5/6 h-auto">
                <div
                    className="flex flex-col items-center gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar/>
                    <div
                        className="h-auto w-[calc(100%-90px)] flex flex-row bg-aliceblue-100 rounded-[20px] text-main-black">
                        <div className="relative w-1/3 m-6 pl-16">
                            <img src={sharp} alt="sharp-img" className="w-56 h-56"/>
                        </div>
                        <div className="relative w-2/3 flex flex-col justify-center items-left gap-3 leading-[1.4]">
                            <p className="h-auto w-2/4 font-inter font-regular text-[20px]">Welcome to the advertising campaign
                                pricing page!</p>
                            <p className="h-auto w-4/5 font-inter font-light text-[14px]">Here you can specify the prices for
                                your services, offer terms of cooperation and describe in detail your offers for brands
                                and companies.</p>
                        </div>
                    </div>
                    <div className="h-auto w-[calc(100%-150px)] relative mt-4 text-main-black">
                        <p className="h-auto w-full font-inter font-light text-[12px] text-left">
                            Fill in information about the cost of different types of advertising, additional services
                            and give potential customers a complete picture of your capabilities and benefits. Make your
                            page attractive and informative to attract more customers!
                        </p>
                    </div>
                    <TermsOfCooperationForm />
                </div>
            </div>
        </div>
    );
}

export default TermsOfCooperationPage;