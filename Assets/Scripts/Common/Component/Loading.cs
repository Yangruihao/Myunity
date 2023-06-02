using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 提交数据等操作，Loading图标旋转动画 - 自定义通用组件
/// </summary>
public class Loading :MonoSingleton<Loading>
{
    // loading 的图片
    public Image image;
    /// <summary>
    /// 加载进度条
    /// </summary>
    public Slider slider;
    /// <summary>
    /// 显示进度文本,提示加载信息
    /// </summary>
    public Text percentText;
    /// <summary>
    /// 异步加载
    /// </summary>
    private AsyncOperation async;
    private float nowProcess = 0;//场景缓冲加载进度

    private void Start()
    {
        StartCoroutine(SceneLoading(DataManager.ActiveScene));
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator SceneLoading(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            //if (async.progress >= 0.9f)
            //{
            //    nowProcess += 1;
            //}


            //slider.value = nowProcess;

            //percentText.text = nowProcess.ToString() + "%";

            //if (nowProcess == 100)
            //{
            //    //
            //    Debug.Log("场景加载完毕");
            //    async.allowSceneActivation = true;
            //}
            //这里为了加载更加流畅对原加载条进行改进，变成伪进度条
            //可以对Time.deltaTime*num中的num数字进行调整，达到想要的加载速度
            nowProcess = Mathf.Lerp(nowProcess, async.progress, Time.deltaTime * 2);
            percentText.text = ((int)(nowProcess / 9 * 10 * 100)).ToString() + "%";
            slider.value = nowProcess / 9 * 10;
            if (nowProcess / 9 * 10 >= 0.995)
            {
                percentText.text = 100.ToString() + "%";
                slider.value = 1;
                async.allowSceneActivation = true;
            }

            yield return new WaitForEndOfFrame();
        }
       
    }
    //void Update()
    //{
    //    //进度图片旋转
    //    //image.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, image.transform.localRotation.eulerAngles.z - 2));
    //}
}
