import TopBar from "../components/TopBar";
import {UsersType} from "../types/UsersType";
import React, {useEffect, useState} from "react";
import {UserType} from "../types/UserType";
import {getUsers} from "../services/user";
import {getCountries} from "../services/countriesAndStates";
import {CountryType} from "../types/CountryType";
import instGreyIcon from "../assets/icons/instagram-grey-icon.svg"
import noProfile from "../assets/icons/no-profile.svg";
import {Link} from "react-router-dom";
import {useUser} from "../hooks/useUser";
import {PaginationHeaders} from "../types/PaginationHeadersType";
import ReactPaginate from "react-paginate";
import iconLeft from "../assets/icons/navigation-chevron-left.svg"
import iconRight from "../assets/icons/navigation-chevron-right.svg"
import {accountCategories} from "../utils/constants";

const SearchBloggersPage: React.FC = () => {
    const { user } = useUser();
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
    const [paginationData, setPaginationData] = useState<PaginationHeaders>({
        currentPage: 1,
        totalPages: 0,
        totalItems: 0,
        hasNextPage: false,
        hasPreviousPage: false,
        itemsPerPage: 10,
    });

    useEffect(() => {
        const fetchCountries = async () => {
            try {
                const countriesData = await getCountries();
                const sortedCountries = (countriesData || []).sort((a, b) =>
                    a.name.localeCompare(b.name)
                );
                setCountries(sortedCountries || []);
            } catch (error) {
                console.error("Error fetching countries:", error);
            }
        };
        fetchCountries();
    }, []);

    useEffect(() => {
        loadUsers();
    }, [filters.pageNumber]);

    const loadUsers = async () => {
        setLoading(true);
        try {
            const activeFilters: Partial<UsersType> = Object.fromEntries(
                Object.entries(filters).filter(([_, value]) => value !== '' && value !== 0)
            );

            const response = await getUsers(activeFilters);

            if (response) {
                setUsers(response.data);
                setPaginationData(response.pagination);
            }
        } catch (error) {
            console.error("Error fetching users:", error);
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
        setFilters({
            ...filters,
            searchTerm: filters.searchTerm|| '',
            pageNumber: 1,
        });
        setUsers([]);
        loadUsers();
    };

    const handlePageChange = ({ selected }: { selected: number }) => {
        setFilters({ ...filters, pageNumber: selected + 1 });
    };

    return (
        <div className="flex flex-col items-center gap-2 bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
            <TopBar user={user} showButton={"off"} />
            <div className="relative w-11/12 flex flex-col gap-12 justify-center items-left">
                <div className="w-full flex flex-row items-center space-x-4">
                    <div className="w-5/6 h-full border border-gray-10 rounded-[5px] flex items-center justify-center p-2">
                        <input
                            type="text"
                            name="searchTerm"
                            placeholder="Search name or description"
                            value={filters.searchTerm || ''}
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
                            name="accountCategory"
                            value={filters.accountCategory}
                            onChange={handleInputChange}
                            className="focus:outline-none h-full w-full bg-transparent"
                        >
                            {[
                                ...accountCategories.filter(category => category.name === "None"),
                                ...accountCategories
                                    .filter(category => category.name !== "None")
                                    .sort((a, b) => a.name.localeCompare(b.name)),
                            ].map(category => (
                                <option key={category.id} value={category.id}>
                                    {category.name}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div
                        className="h-auto w-1/6 border border-gray-10 rounded-[10px] flex items-center justify-center pl-2">
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
                                    <td className="border-b-[1px] border-r-[1px] border-gray-10 p-4">
                                        <Link to={`/profile/${user.id}`}>
                                            {user.firstName} {user.lastName}
                                        </Link>
                                    </td>
                                    <td className="border-b-[1px] border-r-[1px] border-gray-10 text-center flex justify-end items-center">
                                        <img src={instGreyIcon} alt="instagram follower icon" className="w-6 h-6 my-4 mr-1" />
                                        {user.followersCount}
                                    </td>
                                    <td className="border-b-[1px] border-r-[1px] border-gray-10 text-center">
                                        <p>{user.countryName}</p>
                                    </td>
                                    <td className="border-b-[1px] border-r-[1px] border-gray-10 text-right">
                                        <p className="pr-2">{user.mediaCount}</p>
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    )}
                </div>

                <div className="flex justify-start items-center mt-2 mb-16">
                    <ReactPaginate
                        previousLabel={paginationData.currentPage > 1 ? <img alt="left" src={iconLeft} /> : <button  className="pointer-events-none cursor-default"></button >}
                        nextLabel={paginationData.currentPage < paginationData.totalPages ? <img alt="right" src={iconRight}/> :
                            <button className="pointer-events-none cursor-default"></button>}
                        breakLabel={"..."}
                        pageCount={paginationData.totalPages}
                        marginPagesDisplayed={2}
                        pageRangeDisplayed={3}
                        onPageChange={handlePageChange}
                        containerClassName="flex flex-row gap-6"
                        activeClassName="bg-gray-10 text-main-black rounded-full py-2 px-4"
                        pageClassName="p-2 hover:scale-110 active:scale-90 rounded-full cursor-pointer"
                        previousClassName="p-2 hover:scale-110 active:scale-90 rounded-full"
                        nextClassName="p-2 hover:scale-110 active:scale-90 rounded-full"
                    />
                </div>
            </div>
        </div>
    );
};

export default SearchBloggersPage;