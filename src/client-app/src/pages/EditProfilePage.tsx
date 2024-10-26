import {TopBarType} from "../types/TopBarType";
import ProfileEditForm from "../components/Forms/ProfileEditForm";

const EditProfilePage: React.FC = () => {

    return (
        <div className="w-full h-screen flex justify-center justify-start">
        <div className="mr-[1100px] mt-[100px]">
          <ProfileEditForm />
        </div>
      </div>
      
      
    );
};

export default EditProfilePage;