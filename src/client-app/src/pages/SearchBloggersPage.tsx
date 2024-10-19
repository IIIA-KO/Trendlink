import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";
import {UsersType} from "../types/UsersType";
import {useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {getUsers} from "../services/user";
import {getCountries} from "../services/countriesAndStates";
import {CountryType} from "../types/CountryType";
import instGreyIcon from "../assets/icons/instagram-grey-icon.svg"

const SearchBloggersPage: React.FC = () => {

    const [users, setUsers] = useState<UserType[]>([]);
    const [loading, setLoading] = useState(false);
    const [countries, setCountries] = useState<CountryType[]>([]);
    const [filters, setFilters] = useState<UsersType>({
        searchTerm: '',
        sortColumn: 'followersCount',
        sortOrder: 'desc',
        country: '',
        accountCategory: '',
        minFollowersCount: 0,
        minMediaCount: 0,
        pageNumber: 1,
        pageSize: 10,
    });

    useEffect(() => {
        loadUsers();
    }, [filters]);

    useEffect(() => {
        const fetchCountries = async () => {
            try {
                const countriesData = await getCountries();
                setCountries(countriesData || []);
            } catch (error) {
                console.error("Error fetching countries:", error);
            }
        };
        fetchCountries();
    }, []);

    const loadUsers = async () => {
        setLoading(true);
        try {
            const data = await getUsers(filters);
            setUsers(data || []);
        } catch (error) {
            console.error('Error fetching users:', error);
            setUsers([]);
        }
        setLoading(false);
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setFilters({
            ...filters,
            [e.target.name]: e.target.value,
        });
    };

    const handleSearch = () => {
        loadUsers();
    };

    return (
        <div className="bg-background flex justify-start h-auto w-auto">
            <div
                className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-28">
                <Navbar/>
            </div>
            <div className="w-5/6 h-auto">
                <div className="flex flex-col items-center gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                    <TopBar />
                    <div className="relative w-11/12 flex flex-col gap-12 justify-center items-left">
                        <div className="w-full flex flex-row items-center space-x-4">
                            <div className="w-5/6 h-full border border-gray-10 rounded-[5px] flex items-center justify-center p-2">
                                <input
                                    type="text"
                                    name="searchTerm"
                                    placeholder="Search name or description"
                                    value={filters.searchTerm}
                                    onChange={handleInputChange}
                                    className="w-full h-full focus:outline-none"
                                />
                            </div>
                            <button
                                onClick={handleSearch}
                                className="w-1/6 py-2 border-2 border-primary bg-primary text-center text-textPrimary text-[1rem] rounded-[40px] transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
                            >
                                Search
                            </button>
                        </div>

                        <div className="relative flex flex-row space-x-4">
                            <div className="h-auto w-1/6 border border-gray-10 rounded-[10px] flex items-center justify-center pl-2">
                                <select
                                    name="country"
                                    value={filters.country}
                                    onChange={handleInputChange}
                                    className="focus:outline-none h-full w-full bg-transparent"
                                >
                                    <option value="">Select Country</option>
                                    {countries.map((country) => (
                                        <option key={country.id} value={country.name}>
                                            {country.name}
                                        </option>
                                    ))}
                                </select>
                            </div>

                            <div className="h-auto w-1/6 border border-gray-10 rounded-[10px] flex items-center justify-center pl-2">
                                <select
                                    name="accountCategory"
                                    value={filters.accountCategory}
                                    onChange={handleInputChange}
                                    className="focus:outline-none h-full w-full bg-transparent"
                                >
                                    <option value="">Category</option>
                                    <option value="Business">Business</option>
                                    <option value="Lifestyle">Lifestyle</option>
                                    <option value="Fashion">Fashion</option>
                                </select>
                            </div>


                            <div className="h-auto w-1/6 border border-gray-10 rounded-[10px] flex items-center justify-center pl-2">
                                <input
                                    type="number"
                                    name="minFollowersCount"
                                    placeholder="Followers Count"
                                    value={filters.minFollowersCount || ''}
                                    onChange={handleInputChange}
                                    className="focus:outline-none h-full w-full bg-transparent py-2"
                                />
                            </div>

                            <div className="h-auto w-1/6 border border-gray-10 rounded-[10px] flex items-center justify-center pl-2">
                                <input
                                    type="number"
                                    name="minMediaCount"
                                    placeholder="Min Media"
                                    value={filters.minMediaCount || ''}
                                    onChange={handleInputChange}
                                    className="focus:outline-none h-full w-full bg-transparent"
                                />
                            </div>


                            <div className="h-auto w-1/6 border border-gray-10 rounded-[10px] flex items-center justify-center pl-2">
                                <select
                                    name="sortColumn"
                                    value={filters.sortColumn}
                                    onChange={handleInputChange}
                                    className="focus:outline-none h-full w-full bg-transparent"
                                >
                                    <option value="followersCount">Followers</option>
                                    <option value="mediaCount">Media Count</option>
                                </select>
                            </div>

                            <div className="h-auto w-1/6 border border-gray-10 rounded-[10px] flex items-center justify-center pl-2">
                                <select
                                    name="sortOrder"
                                    value={filters.sortOrder}
                                    onChange={handleInputChange}
                                    className="focus:outline-none h-full w-full bg-transparent"
                                >
                                    <option value="asc">Ascending</option>
                                    <option value="desc">Descending</option>
                                </select>
                            </div>
                        </div>

                        <div className="mt-8">
                            {loading ? (
                                <div>Loading...</div>
                            ) : (
                                <table className="table-auto w-full">
                                    <thead>
                                    <tr>
                                        <th className="border-b-[1px] border-r-[1px] border-gray-10 font-inter font-regular text-[14px]">Name</th>
                                        <th className="border-b-[1px] border-r-[1px] border-gray-10 font-inter font-regular text-[14px]">Followers</th>
                                        <th className="border-b-[1px] border-r-[1px] border-gray-10 font-inter font-regular text-[14px]">Country</th>
                                        <th className="border-b-[1px] border-r-[1px] border-gray-10 font-inter font-regular text-[14px]">Media Count</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    {users.map((user) => (
                                        <tr key={user.id}>
                                            <td className="border-b-[1px] border-r-[1px] border-gray-10">{user.firstName} {user.lastName}</td>
                                            <td className="border-b-[1px] border-r-[1px] border-gray-10 text-center flex justify-end items-center"><img src={instGreyIcon} alt="instagram follower icon" className="w-6 h-6 my-4 mr-1"/> {user.followersCount}</td>
                                            <td className="border-b-[1px] border-r-[1px] border-gray-10 text-center"><p>{user.countryName}</p></td>
                                            <td className="border-b-[1px] border-r-[1px] border-gray-10 text-right"><p className="pr-2">{user.mediaCount}</p></td>
                                        </tr>
                                    ))}
                                    <td className="border-r-[1px] border-gray-10 p-2"></td>
                                    <td className="border-r-[1px] border-gray-10 p-2"></td>
                                    <td className="border-r-[1px] border-gray-10 p-2"></td>
                                    <td className="border-r-[1px] border-gray-10 p-2"></td>
                                    </tbody>
                                </table>
                            )}
                        </div>

                        <div className="flex justify-center items-center gap-8 m-12">
                            <button
                                onClick={() =>
                                    setFilters({...filters, pageNumber: (filters.pageNumber || 1) - 1})
                                }
                                disabled={filters.pageNumber === 1}
                                className="w-1/4 h-full py-2 border-2 border-primary bg-primary text-center text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
                            >
                                Previous
                            </button>

                            <button
                                onClick={() =>
                                    setFilters({...filters, pageNumber: (filters.pageNumber || 1) + 1})
                                }
                                className="w-1/4 h-full py-2 border-2 border-primary bg-primary text-center text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
                            >
                                Next
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    );
}

export default SearchBloggersPage;