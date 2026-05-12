import { useOutletContext } from 'react-router-dom';
import PostList from '@/components/PostList';
import ContentUILayout from '@/components/ContentUILayout';

const HomePage = () => {
    // Lấy context từ Outlet của DefaultLayout
    const { activeTab } = useOutletContext<{ activeTab: string }>();

    return (
        <ContentUILayout>
            {activeTab === 'For you' && <PostList type="recommend" />}
            {activeTab === 'Following' && <PostList type="following" />}
            {activeTab === 'Ghost posts' && <PostList type="ghost" />}
        </ContentUILayout>
    );
};

export default HomePage;
