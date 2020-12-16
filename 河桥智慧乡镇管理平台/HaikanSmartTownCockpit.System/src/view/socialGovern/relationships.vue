<template>
  <div>
    <Row style="padding: 15px">
      <div class="demo-upload-list" v-for="item in uploadList">
        <template v-if="item.status === 'finished'">
          <img :src="item.url" />
          <div class="demo-upload-list-cover">
            <Icon type="ios-eye-outline" @click.native="handleView(item.url)"></Icon>
            <Icon type="ios-trash-outline" @click.native="handleRemove(item.name)"></Icon>
          </div>
        </template>
        <template v-else>
          <Progress v-if="item.showProgress" :percent="item.percentage" hide-info></Progress>
        </template>
      </div>
      <Divider dashed />
      <Upload
        ref="upload"
        :show-upload-list="false"
        :default-file-list="defaultList"
        :on-success="showUpResult"
        :on-progress="toUpResult"
        :format="['jpg','jpeg','png']"
        :max-size="5120"
        :data="{fileSavePath:'Relationships'}"
        :on-format-error="handleFormatError"
        :on-exceeded-size="handleMaxSize"
        :before-upload="handleBeforeUpload"
        :headers="postheaders"
        type="drag"
        :action="actionurl"
        style="display: inline-block;width:58px;"
      >
        <div style="width: 58px;height:58px;line-height: 58px;">
          <Icon type="ios-camera" size="20"></Icon>
        </div>
      </Upload>
    </Row>

    <div style="margin-top: 100px">
      <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
    </div>

    <Modal title="查看图片" v-model="visible">
      <img :src="imgName" v-if="visible" style="width: 100%" />
    </Modal>
  </div>
</template>

<script>
  import {
    createRelationships,
    loadRelationships
  } from "@/api/socialGovern/relationships";
  import { getToken } from "@/libs/util";
  import config from "@/config";
    export default {
        name: "relationships",
      data(){
          return{
            uploadList: [],
            defaultList: [],
            actionurl: config.baseUrl.dev + "api/v1/socialgovern/relationships/UpLoadPicture",
            postheaders: "",
            imgName: "",
            loadingStatus: false,
            updisabled: false,
            visible: false,
            formModel: {
              fields: {
                picture: "",
              },
            }
          }
      },
      methods:{
        async showUpResult(response, file, fileList) {

          this.loadingStatus = false;
          this.updisabled = false;
          if (response.code == "200") {
            this.$Message.success(response.message);

            if (this.formModel.fields.picture != null) {
              if (this.formModel.fields.picture.length > 0) {
                this.formModel.fields.picture += ",";
              }
              this.formModel.fields.picture += response.data.fname;
            } else {
              this.formModel.fields.picture = response.data.fname;
            }
            await this.uploadList.push({
              url: config.baseUrl.dev + response.data.strpath.replace("\\", "/"),
              status: "finished",
              name: response.data.strpath,
              fileName: response.data.fname
            });
            // console.log(
            //   (this.$refs.upload.fileList[0].name = e.data.dataPath.split("\\")[1])
            // );
            // console.log(this.defaultfilelist);
            // if (this.departmentlist.length >= 1) {
            //   this.defaultfilelist = [
            //     { name: this.formModel.fields.name, url: e.data.path }
            //   ];
            // }
          } else {
            this.$Message.warning(response.message);
          }
        },
        toUpResult() {

          //console.log(this.$refs.upload.fileList);
          if (this.$refs.upload.fileList.length > 1) {
            this.$refs.upload.fileList.shift();
            // this.$refs.upload.clearFiles();
            // this.$refs.upload.push({})
          }
          this.loadingStatus = true;
          this.updisabled = true;
        },
        handleFormatError(file) {
          this.$Notice.warning({
            title: "The file format is incorrect",
            desc: "文件: " + file.name + " 不是png,jpg"
          });
        },
        handleMaxSize(file) {
          this.$Notice.warning({
            title: "Exceeding file size limit",
            desc: "文件 " + file.name + " 太大,超过5M."
          });
        },
        handleBeforeUpload() {
          // const check = this.uploadList.length < 5;
          // if (!check) {
          //   this.$Notice.warning({
          //     title: "Up to five pictures can be uploaded."
          //   });
          // }
          // return check;
          return true;
        },
        handleView(name) {
          this.imgName = name;
          this.visible = true;
        },
        handleRemove(file) {

          this.uploadList = this.uploadList.filter(x => x.name != file);
          this.formModel.fields.picture = this.uploadList
            .map(x => x.fileName)
            .join(",");

          // const fileList = this.$refs.upload.fileList;
          // this.$refs.upload.fileList.splice(fileList.indexOf(file), 1);
          // deletetoFile({ filePath: file }).then(res => {
          //   if (res.data.code == "200") {
          //     this.uploadList = this.uploadList.filter(x => x.name != file);
          //     this.formModel.fields.picture = this.uploadList
          //       .map(x => x.fileName)
          //       .join(",");
          //   } else {
          //     this.uploadList = this.uploadList.filter(x => x.name != file);
          //     this.formModel.fields.picture = this.uploadList
          //       .map(x => x.fileName)
          //       .join(",");
          //     this.$Message.warning(res.data.message)
          //   }
          // });
        },
        handleSubmitPlan() {
          createRelationships(this.formModel.fields).then(res => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.doloadRelationships();
            } else {
              this.$Message.warning(res.data.message);
            }
          });
        },
        doloadRelationships(){
          this.uploadList=[];
          loadRelationships().then(res=>{
            if(res.data.code==200)
            {
              this.formModel.fields = res.data.data;
              if (res.data.data.picture != null&&res.data.data.picture !="") {
                let list = res.data.data.picture.split(",");
                for (let i = 0; i < list.length; i++) {
                  this.uploadList.push({
                    url:
                      config.baseUrl.dev + "UploadFiles/Relationships/" + list[i],
                    status: "finished",
                    name: "UploadFiles/Relationships/" + list[i],
                    fileName: list[i]
                  });
                }
              }
            }
          })
        }
      },
      mounted() {
        this.doloadRelationships();
      },
      created() {
        this.postheaders = {
          Authorization: "Bearer " + getToken()
          //Accept: "application/json, text/plain, */*"
        };
      }
    }
</script>

<style scoped>
  .demo-upload-list {
    display: inline-block;
    width: 120px;
    height: 120px;
    text-align: center;
    line-height: 120px;
    border: 1px solid transparent;
    border-radius: 4px;
    overflow: hidden;
    background: #fff;
    position: relative;
    box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);
    margin-right: 4px;
  }
  .demo-upload-list img {
    width: 100%;
    height: 100%;
  }
  .demo-upload-list-cover {
    display: none;
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    background: rgba(0, 0, 0, 0.6);
  }
  .demo-upload-list:hover .demo-upload-list-cover {
    display: block;
  }
  .demo-upload-list-cover i {
    color: #fff;
    font-size: 20px;
    cursor: pointer;
    margin: 0 2px;
  }
</style>
