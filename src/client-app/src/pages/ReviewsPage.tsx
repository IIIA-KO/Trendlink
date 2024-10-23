import {useEffect} from "react";
import {useUser} from "../hooks/useUser";
import {useReview} from "../hooks/useReview";

const ReviewsPage: React.FC = () => {
    const { user } = useUser();
    const { userReviews, fetchUserReviews } = useReview();

    useEffect(() => {
        if (user && user.id) {
            fetchUserReviews(user.id, { pageNumber: 1, pageSize: 10 });
        }
    }, [user, fetchUserReviews]);

    return (
        <div>
            <h1>Review Page</h1>

            {user ? (
                <div>
                    <h2>User: {user.firstName} {user.lastName}</h2>
                    <p>Email: {user.email}</p>
                </div>
            ) : (
                <p>Loading user data...</p>
            )}

            {userReviews ? (
                <div>
                    <h2>User Reviews</h2>
                    {userReviews.map((review) => (
                        <div key={review.id}>
                            <p>Rating: {review.rating}</p>
                            <p>Comment: {review.comment}</p>
                            <p>Created On: {new Date(review.createOnUtc).toLocaleString()}</p>
                        </div>
                    ))}
                </div>
            ) : (
                <p>Loading user reviews...</p>
            )}
        </div>
    );
};

export default ReviewsPage;