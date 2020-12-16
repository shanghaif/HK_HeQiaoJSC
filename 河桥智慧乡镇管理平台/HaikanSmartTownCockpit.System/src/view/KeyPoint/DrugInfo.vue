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
                      style="width: 150px"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.userinfo.query.kw"
                      placeholder="请输入姓名"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
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
                  <Button
                    icon="md-refresh"
                    title="刷新"
                    @click="handleRefresh"
                  ></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="添加"
                  >添加</Button
                >
                <Button
                  v-can="'import'"
                  icon="ios-cloud-upload"
                  type="success"
                  @click="handleImportCuisine"
                  title="导入"
                  >导入</Button
                >
                <Button
                  v-can="'export'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCuisine('export')"
                  title="导出"
                  >导出</Button
                >
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCuisineyj('export')"
                  title="一键导出"
                  >一键导出</Button
                >
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
          <template slot-scope="{ row, index }" slot="action">
            <Poptip
              confirm
              :transfer="true"
              title="确定要删除吗?"
              @on-ok="handleDelete(row)"
            >
              <Tooltip
                placement="top"
                content="删除"
                :delay="1000"
                :transfer="true"
              >
                <Button
                  type="error"
                  v-can="'deletes'"
                  size="small"
                  shape="circle"
                  icon="md-trash"
                ></Button>
              </Tooltip>
            </Poptip>
            <Tooltip
              placement="top"
              content="编辑"
              :delay="1000"
              :transfer="true"
            >
              <Button
                v-can="'edit'"
                type="primary"
                size="small"
                shape="circle"
                icon="md-create"
                @click="handleEdit(row)"
              ></Button>
            </Tooltip>
            <Tooltip
              placement="top"
              content="详情"
              :delay="1000"
              :transfer="true"
            >
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
          <FormItem label="姓名" prop="name">
            <Input v-model="formModel.fields.name" placeholder="姓名" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="所属网格" prop="owningGrid">
            <Input
              v-model="formModel.fields.owningGrid"
              placeholder="所属网格"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="性别" prop="sex">
            <Input v-model="formModel.fields.sex" placeholder="性别" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="出生日期" prop="dateBirth">
            <DatePicker
              v-model="formModel.fields.dateBirth"
              @on-change="formModel.fields.dateBirth = $event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="出生日期"
              style="width: 150px"
              :options="options3"
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="身份证" prop="idCard">
            <Input v-model="formModel.fields.idCard" placeholder="身份证" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="毒品来源" prop="sourceDrugs">
            <Input
              v-model="formModel.fields.sourceDrugs"
              placeholder="毒品来源"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="戒毒情况" prop="detoxification">
            <Input
              v-model="formModel.fields.detoxification"
              placeholder="戒毒情况"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="关注程度" prop="attention">
            <Input
              v-model="formModel.fields.attention"
              placeholder="关注程度"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="户籍地址" prop="residenceAddress">
            <Input
              v-model="formModel.fields.residenceAddress"
              placeholder="户籍地址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="户籍地详址" prop="registeredAddress">
            <Input
              v-model="formModel.fields.registeredAddress"
              placeholder="户籍地详址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="户籍派出所" prop="policeStation">
            <Input
              v-model="formModel.fields.policeStation"
              placeholder="户籍派出所"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="房屋编号" prop="housesNumber">
            <Input
              v-model="formModel.fields.housesNumber"
              placeholder="房屋编号"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="现住地址" prop="currentAddress">
            <Input
              v-model="formModel.fields.currentAddress"
              placeholder="现住地址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="无房原因" prop="roomReason">
            <Input
              v-model="formModel.fields.roomReason"
              placeholder="无房原因"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="其他地址" prop="otherAddress">
            <Input
              v-model="formModel.fields.otherAddress"
              placeholder="其他地址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="曾用名" prop="formerName">
            <Input v-model="formModel.fields.formerName" placeholder="曾用名" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="工作单位" prop="employer">
            <Input v-model="formModel.fields.employer" placeholder="工作单位" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="联系电话" prop="phone">
            <Input v-model="formModel.fields.phone" placeholder="联系电话" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="联系手机" prop="contactPhone">
            <Input
              v-model="formModel.fields.contactPhone"
              placeholder="联系手机"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="电子邮件" prop="email">
            <Input v-model="formModel.fields.email" placeholder="电子邮件" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="民族" prop="nation">
            <Input v-model="formModel.fields.nation" placeholder="民族" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="政治面貌" prop="politicalStatus">
            <Input
              v-model="formModel.fields.politicalStatus"
              placeholder="政治面貌"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="文化程度" prop="education">
            <Input
              v-model="formModel.fields.education"
              placeholder="文化程度"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="职业" prop="occupation">
            <Input v-model="formModel.fields.occupation" placeholder="职业" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="婚姻状况" prop="maritalStatus">
            <Input
              v-model="formModel.fields.maritalStatus"
              placeholder="婚姻状况"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="血型" prop="bloodType">
            <Input v-model="formModel.fields.bloodType" placeholder="血型" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="宗教信仰" prop="religious">
            <Input
              v-model="formModel.fields.religious"
              placeholder="宗教信仰"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="身高" prop="height">
            <Input v-model="formModel.fields.height" placeholder="身高" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="吸毒状态" prop="drugStatus">
            <Input
              v-model="formModel.fields.drugStatus"
              placeholder="吸毒状态"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="滥用毒品种类" prop="species">
            <Input
              v-model="formModel.fields.species"
              placeholder="滥用毒品种类"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="管控现状" prop="controlStatus">
            <Input
              v-model="formModel.fields.controlStatus"
              placeholder="管控现状"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="是否服用美沙酮戒毒" prop="detoxificationes">
            <Input
              v-model="formModel.fields.detoxificationes"
              placeholder="是否服用美沙酮戒毒"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="查获日期" prop="seizeddate">
            <Input
              v-model="formModel.fields.seizeddate"
              placeholder="查获日期"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="首吸日期" prop="firstTime">
            <DatePicker
              v-model="formModel.fields.firstTime"
              @on-change="formModel.fields.firstTime = $event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="首吸日期"
              style="width: 150px"
              :options="options3"
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="有无服务成员" prop="waiter">
            <Input
              v-model="formModel.fields.waiter"
              placeholder="有无服务成员"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="最新服务时间" prop="serviceHours">
            <Input
              v-model="formModel.fields.serviceHours"
              placeholder="最新服务时间"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="备注" prop="remarks">
            <Input
              v-model="formModel.fields.remarks"
              type="textarea"
              :row="10"
              placeholder="备注"
            />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleSubmitConsumable"
          >保 存</Button
        >
        <Button
          style="margin-left: 30px"
          icon="md-close"
          @click="formModel.opened = false"
          >取 消</Button
        >
      </div>
    </Drawer>

    <Drawer
      title="详情"
      v-model="formModel1.opened"
      width="600"
      :mask-closable="false"
      :mask="false"
    >
      <Form
        :model="formModel1.fields"
        ref="formdispatch22"
        label-position="top"
      >
        <Row :gutter="16">
          <FormItem label="姓名" prop="name">
            <Input
              v-model="formModel1.fields.name"
              :readonly="true"
              placeholder="姓名"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="所属网格" prop="owningGrid">
            <Input
              v-model="formModel1.fields.owningGrid"
              placeholder="所属网格"
              :readonly="true"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="性别" prop="sex">
            <Input
              v-model="formModel1.fields.sex"
              :readonly="true"
              placeholder="性别"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="出生日期" prop="dateBirth">
            <DatePicker
              v-model="formModel1.fields.dateBirth"
              @on-change="formModel1.fields.dateBirth = $event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="出生日期"
              style="width: 150px"
              :options="options3"
              :disabled="true"
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="身份证" prop="idCard">
            <Input
              v-model="formModel1.fields.idCard"
              :readonly="true"
              placeholder="身份证"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="毒品来源" prop="sourceDrugs">
            <Input
              v-model="formModel1.fields.sourceDrugs"
              :readonly="true"
              placeholder="毒品来源"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="戒毒情况" prop="detoxification">
            <Input
              v-model="formModel1.fields.detoxification"
              :readonly="true"
              placeholder="戒毒情况"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="关注程度" prop="attention">
            <Input
              v-model="formModel1.fields.attention"
              :readonly="true"
              placeholder="关注程度"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="户籍地址" prop="residenceAddress">
            <Input
              v-model="formModel1.fields.residenceAddress"
              :readonly="true"
              placeholder="户籍地址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="户籍地详址" prop="registeredAddress">
            <Input
              v-model="formModel1.fields.registeredAddress"
              :readonly="true"
              placeholder="户籍地详址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="户籍派出所" prop="policeStation">
            <Input
              v-model="formModel1.fields.policeStation"
              :readonly="true"
              placeholder="户籍派出所"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="房屋编号" prop="housesNumber">
            <Input
              v-model="formModel1.fields.housesNumber"
              :readonly="true"
              placeholder="房屋编号"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="现住地址" prop="currentAddress">
            <Input
              v-model="formModel1.fields.currentAddress"
              :readonly="true"
              placeholder="现住地址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="无房原因" prop="roomReason">
            <Input
              v-model="formModel1.fields.roomReason"
              :readonly="true"
              placeholder="无房原因"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="其他地址" prop="otherAddress">
            <Input
              v-model="formModel1.fields.otherAddress"
              :readonly="true"
              placeholder="其他地址"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="曾用名" prop="formerName">
            <Input
              v-model="formModel1.fields.formerName"
              :readonly="true"
              placeholder="曾用名"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="工作单位" prop="employer">
            <Input
              v-model="formModel1.fields.employer"
              :readonly="true"
              placeholder="工作单位"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="联系电话" prop="phone">
            <Input
              v-model="formModel1.fields.phone"
              :readonly="true"
              placeholder="联系电话"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="联系手机" prop="contactPhone">
            <Input
              v-model="formModel1.fields.contactPhone"
              :readonly="true"
              placeholder="联系手机"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="电子邮件" prop="email">
            <Input
              v-model="formModel1.fields.email"
              :readonly="true"
              placeholder="电子邮件"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="民族" prop="nation">
            <Input
              v-model="formModel1.fields.nation"
              :readonly="true"
              placeholder="民族"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="政治面貌" prop="politicalStatus">
            <Input
              v-model="formModel1.fields.politicalStatus"
              :readonly="true"
              placeholder="政治面貌"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="文化程度" prop="education">
            <Input
              v-model="formModel1.fields.education"
              :readonly="true"
              placeholder="文化程度"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="职业" prop="occupation">
            <Input
              v-model="formModel1.fields.occupation"
              :readonly="true"
              placeholder="职业"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="婚姻状况" prop="maritalStatus">
            <Input
              v-model="formModel1.fields.maritalStatus"
              :readonly="true"
              placeholder="婚姻状况"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="血型" prop="bloodType">
            <Input
              v-model="formModel1.fields.bloodType"
              :readonly="true"
              placeholder="血型"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="宗教信仰" prop="religious">
            <Input
              v-model="formModel1.fields.religious"
              :readonly="true"
              placeholder="宗教信仰"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="身高" prop="height">
            <Input
              v-model="formModel1.fields.height"
              :readonly="true"
              placeholder="身高"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="吸毒状态" prop="drugStatus">
            <Input
              v-model="formModel1.fields.drugStatus"
              :readonly="true"
              placeholder="吸毒状态"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="滥用毒品种类" prop="species">
            <Input
              v-model="formModel1.fields.species"
              :readonly="true"
              placeholder="滥用毒品种类"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="管控现状" prop="controlStatus">
            <Input
              v-model="formModel1.fields.controlStatus"
              :readonly="true"
              placeholder="管控现状"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="是否服用美沙酮戒毒" prop="detoxificationes">
            <Input
              v-model="formModel1.fields.detoxificationes"
              :readonly="true"
              placeholder="是否服用美沙酮戒毒"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="查获日期" prop="seizeddate">
            <Input
              v-model="formModel1.fields.seizeddate"
              :readonly="true"
              placeholder="查获日期"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="首吸日期" prop="firstTime">
            <DatePicker
              v-model="formModel1.fields.firstTime"
              @on-change="formModel1.fields.firstTime = $event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="首吸日期"
              style="width: 150px"
              :options="options3"
              :disabled="true"
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="有无服务成员" prop="waiter">
            <Input
              v-model="formModel1.fields.waiter"
              :readonly="true"
              placeholder="有无服务成员"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="最新服务时间" prop="serviceHours">
            <Input
              v-model="formModel1.fields.serviceHours"
              :readonly="true"
              placeholder="最新服务时间"
            />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="备注" prop="remarks">
            <Input
              v-model="formModel1.fields.remarks"
              :readonly="true"
              type="textarea"
              :row="10"
              placeholder="备注"
            />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button
          style="margin-left: 30px"
          icon="md-close"
          @click="formModel1.opened = false"
          >取 消</Button
        >
      </div>
    </Drawer>
    <Drawer
      title="吸毒人员信息导入(导入信息时,性别和生日自动从身份证获取,请保证身份证输入正确)"
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
          title="吸毒人员信息导入模板下载"
          >吸毒人员信息导入模板下载</Button
        >
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleimport"
          :disabled="importdisable"
          >导入</Button
        >

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
  GetList, //显示列表
  GetCreate, //新增
  GetfoGet, //获取选定信息
  GetEdit, //编辑
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  GetImport, //导入
  GetExport, //导出
} from "@/api/KeyPoint/DrugInfo";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "DrugInfo",
  components: {
    DzTable,
  },
  data() {
    return {
      options3: {
        disabledDate(date) {
          return date && date.valueOf() > Date.now();
        },
      },
      //导入
      url: config.baseUrl.dev,
      importdisable: false,

      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false,
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
        export: { name: "export", title: "导出" },
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
                field: "ID",
              },
            ],
          },
          sources: {
            sexList: [
              {
                value: "正常营业",
                label: "正常营业",
              },
              {
                value: "暂停营业",
                label: "暂停营业",
              },
            ],
          },
          //列表显示
          columns: [
            { type: "selection", minwidth: 50, key: "drugUuid" },
            {
              title: "姓名",
              key: "name",
              minWidth: 120,
              sortable: true,
            },
            {
              title: "所属网格",
              key: "owningGrid",
              minWidth: 150,
              sortable: true,
            },
            {
              title: "身份证",
              key: "idCard",
              minWidth: 120,
              sortable: true,
            },
            {
              title: "联系电话",
              key: "phone",
              minWidth: 120,
              sortable: true,
            },
            {
              title: "戒毒情况",
              key: "detoxification",
              minWidth: 150,
              sortable: true,
            },
            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 100,
              className: "table-command-column",
              slot: "action",
            },
          ],
          data: [],
        },
      },
      formModel: {
        opened: false,
        title: "创建申请",
        mode: "create",
        dFileName: "xxxx",
        selection: [],
        fields: {
          name: "",
          owningGrid: "",
          idCard: "",
          phone: "",
          sex: "",
          dateBirth: "",
          idCard: "",
          sourceDrugs: "",
          detoxification: "",
          residenceAddress: "",
          registeredAddress: "",
          policeStation: "",
          housesNumber: "",
          currentAddress: "",
          roomReason: "",
          otherAddress: "",
          formerName: "",
          employer: "",
          contactPhone: "",
          email: "",
          nation: "",
          politicalStatus: "",
          education: "",
          occupation: "",
          maritalStatus: "",
          bloodType: "",
          religious: "",
          height: "",
          drugStatus: "",
          species: "",
          controlStatus: "",
          detoxificationes: "",
          seizeddate: "",
          firstTime: "",
          waiter: "",
          serviceHours: "",
          remarks: "",
        },
        rules: {
          name: [{ type: "string", required: true, message: "请输入姓名" }],
        },
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          name: "",
          owningGrid: "",
          idCard: "",
          phone: "",
          sex: "",
          dateBirth: "",
          idCard: "",
          sourceDrugs: "",
          detoxification: "",
          residenceAddress: "",
          registeredAddress: "",
          policeStation: "",
          housesNumber: "",
          currentAddress: "",
          roomReason: "",
          otherAddress: "",
          formerName: "",
          employer: "",
          contactPhone: "",
          email: "",
          nation: "",
          politicalStatus: "",
          education: "",
          occupation: "",
          maritalStatus: "",
          bloodType: "",
          religious: "",
          height: "",
          drugStatus: "",
          species: "",
          controlStatus: "",
          detoxificationes: "",
          seizeddate: "",
          firstTime: "",
          waiter: "",
          serviceHours: "",
          remarks: "",
        },
      },
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
      return this.formModel.selection.map((x) => x.drugUuid);
    }, //删除
  },
  methods: {
    //页面加载
    loadDispatchList() {
      GetList(this.stores.userinfo.query).then((res) => {
        //console.log(res.data.data);
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
      this.doDelete(row.drugUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteDepartment(ids).then((res) => {
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
        },
      });
    },
    //右上边删除
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(","),
      }).then((res) => {
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
      this.doLoadData(row.drugUuid);
    },
    //右边详情
    handleDetail(row) {
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(row.drugUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      GetfoGet(id).then((res) => {
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
      this.$refs["formdispatch"].validate((valid) => {
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
      if (this.formModel.fields.name != "") {
        let reg = /^[^\s]+$/;
        if (!reg.test(this.formModel.fields.name)) {
          this.$Message.warning("姓名不合法!");
          return;
        }
      } else {
        this.$Message.warning("姓名不能为空!");
        return;
      }
      if (this.formModel.fields.sex != "") {
        if (this.formModel.fields.sex != "男" && this.formModel.fields.sex != "女") {
          this.$Message.warning("性别为男或女!");
          return;
        }
      }
      if (
        this.formModel.fields.idCard != null &&
        this.formModel.fields.idCard != ""
      ) {
        let reg1 = /^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$/;
        if (!reg1.test(this.formModel.fields.idCard)) {
          this.$Message.warning("身份证号不合法!");
          return;
        }
      }
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
      GetCreate(this.formModel.fields).then((res) => {
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
      //this.datadeal();
      GetEdit(this.formModel.fields).then((res) => {
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
        this.url + "UploadFiles/KeyPointModel/吸毒人员信息导入模板.xlsx";
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
        await GetImport(this.exceldata).then((res) => {
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
        },
      });
    },
    doCuisineExport(command) {
      console.log(this.selectedRowsId.join(","));
      GetExport(this.selectedRowsId.join(",")).then((res) => {
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
        },
      });
    },
  },
  mounted() {
    this.loadDispatchList(); //页面加载
  },
  created() {
    this.postheaders = {
      Authorization: "Bearer " + getToken(),
      //Accept: "application/json, text/plain, */*"
    };
    this.actionurl = config.baseUrl.dev + "api/v1/HqCommuna/HqCommuna/UpLoad";
  },
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