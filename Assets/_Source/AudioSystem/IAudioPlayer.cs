namespace AudioSystem
{
    public interface IAudioPlayer
    {
        AudioType AudioType { get; }
        void SetVolume(float volume);
    }
}