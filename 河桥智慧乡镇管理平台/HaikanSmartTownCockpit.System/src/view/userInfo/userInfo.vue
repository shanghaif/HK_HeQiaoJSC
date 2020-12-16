<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.userinfo.query.totalCount"
        :pageSize="stores.userinfo.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.userinfo.query.kw"
                      placeholder="请输入姓名"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                  <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.userinfo.query.kw1"
                      placeholder="身份证号"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                  <!-- <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.userinfo.query.kw2"
                      placeholder="请输入学历"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem> -->
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
                    v-can="'delete'"
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="添加"
                >添加</Button>
                <Button
                  v-can="'import'"
                  icon="ios-cloud-upload"
                  type="success"
                  @click="handleImportCuisine"
                  title="导入"
                >导入</Button>
                <Button
                  v-can="'export'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCuisine('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCuisineyj('export')"
                  title="一键导出"
                >一键导出</Button>
              </Col>
            </Row>
          </section>
        </div>
        <Table
          slot="table"
          ref="tables"
          :border="false"
          size="small"
          :highlight-row="true"
          :data="stores.userinfo.data"
          :columns="stores.userinfo.columns"
          @on-select="handleSelect"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <!-- <template slot-scope="{row, index}" slot="auditState">
            <span>{{renderAuditState(row.auditState)}}</span>
          </template>-->
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button type="error" v-can="'deletes'" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
              <Button
                v-can="'edit'"
                type="primary"
                size="small"
                shape="circle"
                icon="md-create"
                @click="handleEdit(row)"
              ></Button>
            </Tooltip>
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleDetail(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
    >
      <Form
        :model="formModel.fields"
        ref="formdispatch"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="姓名" prop="realName">
              <Input v-model="formModel.fields.realName" placeholder="姓名" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="性别" prop="sex">
              <Select
                filterable
                v-model="formModel.fields.sex"
                style="width:180px"
                placeholder="性别"
              >
                <Option
                  v-for="item in stores.userinfo.sources.sexList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <!-- <FormItem label="学历" prop="education">
              <Select
                filterable
                v-model="formModel.fields.education"
                style="width:180px"
                placeholder="学历"
              >
                <Option
                  v-for="item in stores.userinfo.sources.xueliList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem> -->
            <FormItem label="户别" prop="household">
              <Input v-model="formModel.fields.household" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="政治面貌" prop="politics">
              <Select
                filterable
                v-model="formModel.fields.politics"
                style="width:180px"
                placeholder="政治面貌"
              >
                <Option
                  v-for="item in stores.userinfo.sources.zhengzhiList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="民族" prop="nation">
              <Select
                filterable
                v-model="formModel.fields.nation"
                style="width:180px"
                placeholder="民族"
              >
                <Option
                  v-for="item in stores.userinfo.sources.minzuList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="出生日期" prop="birth">
              <DatePicker
                v-model="formModel.fields.birth"
                @on-change="formModel.fields.birth=$event"
                format="yyyy-MM-dd"
                type="date"
                placeholder="出生日期"
                style="width: 150px"
                :options="options3"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="身份证号" prop="identityCard">
              <Input v-model="formModel.fields.identityCard" placeholder="身份证号" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="电话号码" prop="phone">
              <Input v-model="formModel.fields.phone" placeholder="电话号码" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="户籍地" prop="residence">
              <Input v-model="formModel.fields.residence" placeholder="户籍地" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="地址" prop="domicile">
              <Input v-model="formModel.fields.domicile" placeholder="地址" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="户主姓名" prop="householderName">
              <Input v-model="formModel.fields.householderName" placeholder="户主姓名" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="与户主的关系" prop="relation">
              <Input v-model="formModel.fields.relation" placeholder="与户主的关系" />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="迁入时间" prop="qianYiStime">
              <DatePicker
                v-model="formModel.fields.qianYiStime"
                @on-change="formModel.fields.qianYiStime=$event"
                format="yyyy-MM-dd"
                type="date"
                placeholder="迁入时间"
                style="width: 150px"
              ></DatePicker>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="迁出时间" prop="qianYiEtime">
              <DatePicker
                v-model="formModel.fields.qianYiEtime"
                @on-change="formModel.fields.qianYiEtime=$event"
                format="yyyy-MM-dd"
                type="date"
                placeholder="迁出时间"
                style="width: 150px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable">保 存</Button>
        <Button style="margin-left: 30px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer title="详情" v-model="formModel1.opened" width="600" :mask-closable="false" :mask="false">
      <Form :model="formModel1.fields" ref="formdispatch22" label-position="top">
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="姓名" prop="realName">
              <Input v-model="formModel1.fields.realName" :readonly="true" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="性别" prop="sex">
              <Input v-model="formModel1.fields.sex" :readonly="true" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <!-- <FormItem label="学历" prop="education">
              <Input v-model="formModel1.fields.education" :readonly="true" />
            </FormItem> -->
            <FormItem label="户别" prop="household">
              <Input v-model="formModel1.fields.household" :readonly="true" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="政治面貌" prop="politics">
              <Input v-model="formModel1.fields.politics" :readonly="true" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="民族" prop="nation">
              <Input v-model="formModel1.fields.nation" :readonly="true" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="出生日期" prop="birth">
              <!-- <Input v-model="formModel1.fields.birth" :readonly="true" /> -->
              <DatePicker
                v-model="formModel1.fields.birth"
                format="yyyy-MM-dd"
                type="date"
                placeholder="出生日期"
                style="width: 150px"
                disabled
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="身份证号" prop="identityCard">
              <Input v-model="formModel1.fields.identityCard" :readonly="true" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="电话号码" prop="phone">
              <Input v-model="formModel1.fields.phone" :readonly="true" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="户籍地" prop="residence">
              <Input v-model="formModel1.fields.residence" :readonly="true" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="地址" prop="domicile">
              <Input v-model="formModel1.fields.domicile" :readonly="true" />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="迁入时间" prop="qianYiStime">
              <!-- <Input v-model="formModel1.fields.qianYiTime" :readonly="true" /> -->
              <DatePicker
                v-model="formModel1.fields.qianYiStime"
                format="yyyy-MM-dd"
                type="date"
                placeholder="迁入时间"
                style="width: 150px"
                disabled
              ></DatePicker>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="迁出时间" prop="qianYiEtime">
              <!-- <Input v-model="formModel1.fields.qianYiTime" :readonly="true" /> -->
              <DatePicker
                v-model="formModel1.fields.qianYiEtime"
                format="yyyy-MM-dd"
                type="date"
                placeholder="迁出时间"
                style="width: 150px"
                disabled
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button style="margin-left: 30px" icon="md-close" @click="formModel1.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="人员信息导入"
      v-model="formimport.opened"
      width="600"
      :mask-closable="true"
      :mask="true"
    >
      <div>
        <input
          ref="inputer"
          id="upload"
          type="file"
          @change="importfxx"
          accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
        />
        <Button
          icon="ios-cloud-download"
          type="warning"
          @click="handleimportmodel"
          title="人员信息导入模板下载"
        >人员信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleimport"
          :disabled="importdisable"
        >导入</Button>

        <Tabs value="name1">
          <TabPane label="成功" name="name1" v-html="successmsg"></TabPane>
          <TabPane label="重复" name="name2" v-html="repeatmsg"></TabPane>
          <TabPane label="失败" name="name3" v-html="defaultmsg"></TabPane>
        </Tabs>
      </div>
    </Drawer>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import {
  UserInfoList, //显示列表
  UserInfoCreate, //新增
  UserInfoGet, //获取选定信息
  UserInfoEdit, //编辑
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  UserInfoImport, //导入
  UserInfoExport //导出
} from "@/api/userInfo/userInfo";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "userInfo",
  components: {
    DzTable
  },
  data() {
    return {
      options3: {
        disabledDate(date) {
          return date && date.valueOf() > Date.now();
        }
      },
      //导入
      url: config.baseUrl.dev,
      importdisable: false,

      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false
      },

      actionurl: "",
      postheaders: "",
      loadingStatus: false,
      updisabled: false,
      visible: false,
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        Import: { name: "Import", title: "导入" },
        export: { name: "export", title: "导出" }
      },
      inglist: [],
      model1: [],
      model2: [],
      stores: {
        userinfo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kw1: "",
            kw2: "",
            isDelete: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID"
              }
            ]
          },
          sources: {
            sexList: [
              {
                value: "男",
                label: "男"
              },
              {
                value: "女",
                label: "女"
              }
            ],
            xueliList: [
              {
                value: "小学",
                label: "小学"
              },
              {
                value: "初中",
                label: "初中"
              },
              {
                value: "中专",
                label: "中专"
              },
              {
                value: "大专",
                label: "大专"
              },
              {
                value: "本科",
                label: "本科"
              },
              {
                value: "硕士",
                label: "硕士"
              },
              {
                value: "博士",
                label: "博士"
              }
            ],
            zhengzhiList: [
              {
                value: "群众",
                label: "群众"
              },
              {
                value: "中共党员",
                label: "中共党员"
              },
              {
                value: "中共预备党员",
                label: "中共预备党员"
              },
              {
                value: "共青团员",
                label: "共青团员"
              },
              {
                value: "民革党员",
                label: "民革党员"
              },
              {
                value: "民盟盟员",
                label: "民盟盟员"
              },
              {
                value: "民建会员",
                label: "民建会员"
              },
              {
                value: "民进工会",
                label: "民进工会"
              },
              {
                value: "农工党党员",
                label: "农工党党员"
              },
              {
                value: "致公党党员",
                label: "致公党党员"
              },
              {
                value: "九三学社社员",
                label: "九三学社社员"
              },
              {
                value: "台盟盟员",
                label: "台盟盟员"
              },
              {
                value: "无党派人士",
                label: "无党派人士"
              }
            ],
            minzuList: [
              {
                value: "汉族",
                label: "汉族"
              },
              {
                value: "蒙古族",
                label: "蒙古族"
              },
              {
                value: "回族",
                label: "回族"
              },
              {
                value: "藏族",
                label: "藏族"
              },
              {
                value: "维吾尔族",
                label: "维吾尔族"
              },
              {
                value: "苗族",
                label: "苗族"
              },
              {
                value: "彝族",
                label: "彝族"
              },
              {
                value: "壮族",
                label: "壮族"
              },
              {
                value: "布依族",
                label: "布依族"
              },
              {
                value: "朝鲜族",
                label: "朝鲜族"
              },
              {
                value: "满族",
                label: "满族"
              },
              {
                value: "侗族",
                label: "侗族"
              },
              {
                value: "瑶族",
                label: "瑶族"
              },
              {
                value: "白族",
                label: "白族"
              },
              {
                value: "土家族",
                label: "土家族"
              },
              {
                value: "哈尼族",
                label: "哈尼族"
              },
              {
                value: "哈萨克族",
                label: "哈萨克族"
              },
              {
                value: "傣族",
                label: "傣族"
              },
              {
                value: "黎族",
                label: "黎族"
              },
              {
                value: "僳僳族",
                label: "僳僳族"
              },
              {
                value: "佤族",
                label: "佤族"
              },
              {
                value: "畲族",
                label: "畲族"
              },
              {
                value: "高山族",
                label: "高山族"
              },
              {
                value: "拉祜族",
                label: "拉祜族"
              },
              {
                value: "水族",
                label: "水族"
              },
              {
                value: "东乡族",
                label: "东乡族"
              },
              {
                value: "纳西族",
                label: "纳西族"
              },
              {
                value: "景颇族",
                label: "景颇族"
              },
              {
                value: "柯尔克孜族",
                label: "柯尔克孜族"
              },
              {
                value: "土族",
                label: "土族"
              },
              {
                value: "达斡尔族",
                label: "达斡尔族"
              },
              {
                value: "仫佬族",
                label: "仫佬族"
              },
              {
                value: "羌族",
                label: "羌族"
              },
              {
                value: "布朗族",
                label: "布朗族"
              },
              {
                value: "撒拉族",
                label: "撒拉族"
              },
              {
                value: "毛南族",
                label: "毛南族"
              },
              {
                value: "仡佬族",
                label: "仡佬族"
              },
              {
                value: "锡伯族",
                label: "锡伯族"
              },
              {
                value: "阿昌族",
                label: "阿昌族"
              },
              {
                value: "普米族",
                label: "普米族"
              },
              {
                value: "塔吉克族",
                label: "塔吉克族"
              },
              {
                value: "怒族",
                label: "怒族"
              },
              {
                value: "乌孜别克族",
                label: "乌孜别克族"
              },
              {
                value: "俄罗斯族",
                label: "俄罗斯族"
              },
              {
                value: "鄂温克族",
                label: "鄂温克族"
              },
              {
                value: "德昂族",
                label: "德昂族"
              },
              {
                value: "保安族",
                label: "保安族"
              },
              {
                value: "裕固族",
                label: "裕固族"
              },
              {
                value: "京族",
                label: "京族"
              },
              {
                value: "独龙族",
                label: "独龙族"
              },
              {
                value: "鄂伦春族",
                label: "鄂伦春族"
              },
              {
                value: "赫哲族",
                label: "赫哲族"
              },
              {
                value: "门巴族",
                label: "门巴族"
              },
              {
                value: "珞巴族",
                label: "珞巴族"
              },
              {
                value: "基诺族",
                label: "基诺族"
              }
            ]
          },
          //列表显示
          columns: [
            { type: "selection", minwidth: 50, key: "userInfoUuid" },
            { title: "姓名", key: "realName", minWidth: 80, sortable: true },
            { title: "性别", key: "sex", minWidth: 80, sortable: true },
            { title: "出生日期", key: "birth", minWidth: 90, sortable: true },
            {
              title: "身份证号",
              key: "identityCard",
              minWidth: 90,
              sortable: true
            },
            { title: "户籍地", key: "residence", minWidth: 100,  },
            { title: "地址", key: "domicile", minWidth: 100,  },
            { title: "民族", key: "nation", minWidth: 80,  },
            { title: "户别", key: "household", minWidth: 90,  },
            { title: "电话号码", key: "phone", minWidth: 80,  },
            { title: "与户主的关系", key: "relation", minWidth: 100 },
            { title: "户主姓名", key: "householderName", minWidth: 90,  },

            //{ title: "学历", key: "education", minWidth: 80, sortable: true },
            { title: "迁入时间", key: "qianYiStime", minWidth: 80,  },
            {
              title: "迁出时间",
              key: "qianYiEtime",
              minWidth: 90,
              sortable: true
            },
            //{ title: "职业", key: "occupation", minWidth: 80, sortable: true },
            {
              title: "政治面貌",
              key: "politics",
              minWidth: 80,
              
            },

            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 100,

              className: "table-command-column",
              slot: "action"
            }
          ],
          data: []
        }
      },
      formModel: {
        opened: false,
        title: "创建申请",
        mode: "create",
        selection: [],
        fields: {
          realName: "",
          address: "",
          phone: "",
          email: "",
          sex:"",
          birth: "",
          identityCard: "",
          residence: "",
          domicile: "",
          nation: "",
          education: "",
          qianYiStime: "",
          qianYiEtime: "",
          occupation: "",
          dyStaues: "",
          politics: "",
          position: "",
          evaluate: "",
          joinArmy: "",
          armyTime: "",
          userInfoUuid: "",
          household:"",
          relation:"",
          householderName:"",
        },
        rules: {
          // realName: [
          //   { type: "string", required: true, message: "姓名不能为空" }
          // ]
        }
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          realName: "",
          address: "",
          phone: "",
          email: "",
          birth: "",
          sex:"",
          identityCard: "",
          residence: "",
          domicile: "",
          nation: "",
          education: "",
          qianYiStime: "",
          qianYiEtime: "",
          occupation: "",
          dyStaues: "",
          politics: "",
          position: "",
          evaluate: "",
          joinArmy: "",
          armyTime: "",
          userInfoUuid: "",
          household:"",
          relation:"",
          householderName:"",
        }
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增信息";
      }
      if (this.formModel.mode === "edit") {
        return "编辑信息";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.userInfoUuid);
    } //删除
  },
  methods: {
    //页面加载
    loadDispatchList() {
      UserInfoList(this.stores.userinfo.query).then(res => {
        console.log(res.data.data);
        this.stores.userinfo.data = res.data.data;
        this.stores.userinfo.query.totalCount = res.data.totalCount;
      });
    },
    handleSelect(selection, row) {},
    //多选
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.userinfo.query.currentPage = page;
      this.loadDispatchList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.userinfo.query.pageSize = pageSize;
      this.loadDispatchList();
    },
    //行样式
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //搜索
    handleSearchDispatch() {
      this.loadDispatchList();
    },
    //刷新
    handleRefresh() {
      this.loadDispatchList();
    },
    //清空
    handleResetFormDispatch() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch"].resetFields();
    },
    //清空
    handleResetFormDispatch22() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch22"].resetFields();
    },
    //右边删除（单个删除）
    handleDelete(row) {
      this.doDelete(row.userInfoUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteDepartment(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //右上边删除按钮
    handleBatchCommand(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doBatchCommand(command);
        }
      });
    },
    //右上边删除
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },

    xz(e) {
      this.stores.userinfo.query.kw = e;
      this.loadDispatchList();
    },
    //添加按钮
    handleShowCreateWindow() {
      this.formModel.mode = "create";
      this.handleResetFormDispatch(); //清空表单
      this.formModel.opened = true;
    },
    //右边编辑
    handleEdit(row) {
      this.formModel.mode = "edit";
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.userInfoUuid);
    },
    //右边详情
    handleDetail(row) {
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(row.userInfoUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      UserInfoGet(id).then(res => {
        if (res.data.code === 200) {
          console.log(res.data.data);
          let data = res.data.data[0];
          this.formModel.fields = data;
          this.formModel1.fields = data;
        }
      });
    },
    validateUserForm() {
      let _valid = false;
      this.$refs["formdispatch"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //保存按钮
    handleSubmitConsumable() {
      let valid = this.validateUserForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateDispatch();
        }
        if (this.formModel.mode === "edit") {
          this.doEditDispatch();
        }
      }
    },
    //添加（保存）
    docreateDispatch() {
      // if (this.formModel.fields.realName != "") {
      //   let reg = /^[^\s]+$/;
      //   if (!reg.test(this.formModel.fields.realName)) {
      //     this.$Message.warning("姓名不合法!");
      //     return;
      //   }
      // } else {
      //   this.$Message.warning("姓名不能为空!");
      //   return;
      // }
      // if (this.formModel.fields.identityCard != null && this.formModel.fields.identityCard != "") {
      //   let reg1 = /^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$/;
      //   if (!reg1.test(this.formModel.fields.identityCard)) {
      //     this.$Message.warning("身份证号不合法!");
      //     return;
      //   }
      // }
      console.log(this.formModel.fields);
      UserInfoCreate(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑（保存）
    doEditDispatch() {
      console.log(this.formModel.fields);
      // if (this.formModel.fields.realName != "") {
      //   let reg = /^[^\s]+$/;
      //   if (!reg.test(this.formModel.fields.realName)) {
      //     this.$Message.warning("姓名不合法!");
      //     return;
      //   }
      // } else {
      //   this.$Message.warning("姓名不能为空!");
      //   return;
      // }
      // if (this.formModel.fields.identityCard != null && this.formModel.fields.identityCard != "") {
      //   let reg1 = /^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$/;
      //   if (!reg1.test(this.formModel.fields.identityCard)) {
      //     this.$Message.warning("身份证号不合法!");
      //     return;
      //   }
      // }
      //this.datadeal();
      UserInfoEdit(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //导入相关操作
    handleImportCuisine() {
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      this.$refs.inputer.value = "";
      this.isexitexcel = false;
      this.formimport.opened = true;
    },
    //下载模板
    handleimportmodel() {
      console.log(this.url);
      window.location.href =
        this.url + "UploadFiles/ImportUserInfoModel/人员信息导入模板.xlsx";
    },
    //导入
    importfxx(e) {
      let inputDOM = this.$refs.inputer;
      //声明一个FormDate对象
      let formData = new FormData();
      let file = inputDOM.files[0];
      let AllUpExt = ".xls|.xlsx|";
      let extName = file.name
        .substring(file.name.lastIndexOf("."))
        .toLowerCase();
      if (AllUpExt.indexOf(extName + "|") == "-1") {
        this.$refs.inputer.value = "";
        this.$Message.warning("文件格式不正确!");
      } else {
        if (file != null && file != undefined) {
          this.isexitexcel = true;
          formData.append("excelFile", file);
          this.exceldata = formData;
        } else {
          this.isexitexcel = false;
        }
      }
    },
    async handleimport() {
      this.importdisable = true;
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      if (this.isexitexcel) {
        await UserInfoImport(this.exceldata).then(res => {
          if (res.data.code == 200) {
            this.time = res.data.data.time;
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.loadDispatchList();
          } else {
            this.$Message.warning(res.data.message);
          }
          this.$refs.inputer.value = "";
          this.exceldata = new FormData();
          this.isexitexcel = false;
        });
      } else {
        this.$Message.warning("请选择文件");
      }
      this.importdisable = false;
    },
    //导出
    handleExportCuisine(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doCuisineExport(command);
        }
      });
    },
    doCuisineExport(command) {
      console.log(this.selectedRowsId.join(","));
      UserInfoExport(this.selectedRowsId.join(",")).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.selection = [];
          console.log(res);
          window.location.href = config.baseUrl.dev + res.data.data;
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    //一键导出
    handleExportCuisineyj(command) {
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doCuisineExport(command);
        }
      });
    }
  },
  mounted() {
    this.loadDispatchList(); //页面加载
  },
  created() {
    this.postheaders = {
      Authorization: "Bearer " + getToken()
      //Accept: "application/json, text/plain, */*"
    };
    this.actionurl = config.baseUrl.dev + "api/v1/UserInfo/UserInfo/UpLoad";
  }
};
</script>
<style>
.file1:hover + .fileimg1:hover {
  transform: scale(1.01, 1.01);
  box-shadow: 0 0 3px #1783ba;
}
.fileimg1 {
  width: 300px;
  height: 169px;
  float: left;
  z-index: 2;
}

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