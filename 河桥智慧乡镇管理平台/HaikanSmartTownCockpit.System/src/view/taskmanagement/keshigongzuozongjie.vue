<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.vote.query.totalCount"
        :pageSize="stores.vote.query.pageSize"
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
                      type="text"
                      style="width: 180px;"
                      :clearable="true"
                      v-model="stores.vote.query.kwbt"
                      placeholder="输入标题搜索..."
                    ></Input>
                  </FormItem>
                  <FormItem>
                    <Select
                      v-model="stores.vote.query.kwfzr"
                      :clearable="true"
                      style="width: 180px;"
                      placeholder="请选择部门"
                      filterable
                    >
                      <Option
                        v-for="item in keshidatalist"
                        :value="item.priorityHeadline"
                        :key="item.priorityUUID"
                      >{{item.priorityHeadline}}</Option>
                    </Select>
                  </FormItem>
                  <FormItem>
                    <DatePicker
                      type="date"
                      style="width: 180px;"
                      :value="stores.vote.query.kwendtime"
                      @on-change="stores.vote.query.kwendtime=$event"
                      format="yyyy-MM-dd"
                      placeholder="选择撰写时间"
                    ></DatePicker>
                  </FormItem>
                  <FormItem>
                    <Button
                      v-can="'search'"
                      icon="md-search"
                      type="primary"
                      @click="handleSearch()"
                      title="查询"
                    >查询</Button>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
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
                  title="新增总结"
                >新增总结</Button>
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
          :data="stores.vote.data"
          :columns="stores.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
          @on-sort-change="handleSortChange"
        >
          <template slot-scope="{row,index}" slot="auditStatus">
            <span>{{getauditStatus(row.auditStatus)}}</span>
          </template>
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="您确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button v-can="'delete'" type="error" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="success"
                size="small"
                shape="circle"
                icon="md-search"
                @click="lookEwq(row)"
              ></Button>
            </Tooltip>
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
            <Tooltip
              placement="top"
              content="完成"
              :delay="1000"
              v-if="wanchengshow(row)"
              :transfer="true"
            >
              <Button
                v-can="'accomplish'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-checkmark"
                @click="wancheng(row)"
              ></Button>
            </Tooltip>
            <Tooltip placement="top" content="审批" :delay="1000" :transfer="true">
              <Button
                v-can="'approve'"
                type="primary"
                size="small"
                shape="circle"
                icon="md-chatboxes"
                v-if="shenpishow(row)"
                @click="shenpitask(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>

    <!-- 添加、修改的表单 -->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModel.fields" ref="formtask" :rules="formModel.rules" label-position="top">
        <Row :gutter="16">
          <Col span="12">
            <!--<FormItem label="所属重点工作" prop="priorityUUID">
		<Select v-model="formModel.fields.priorityUUID" placeholder="所属重点工作">
			<Option v-for="item in prioritydata" :value="item.priorityUUID" :title="item.priorityHeadlinelong">{{item.priorityHeadline}}</Option>
            </Select>-->
            <FormItem label="科室" prop="administrativeOffice">
              <Select v-model="formModel.fields.administrativeOffice" placeholder="科室" filterable>
                <Option
                  v-for="item in keshidata"
                  :value="item.priorityUUID"
                  :title="item.priorityHeadlinelong"
                >{{item.priorityHeadline}}</Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="标题" prop="missionHeadline">
              <Input v-model="formModel.fields.missionHeadline" placeholder="请输入标题" />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="撰写时间" prop="startTime">
              <Input v-model="formModel.fields.startTime" readonly="true" />
            </FormItem>
          </Col>
          <!-- <Col span="12">
            <FormItem label="结束时间" prop="finishTime">
              <Date-picker
                type="datetime"
                format="yyyy-MM-dd HH:mm"
                :options="options3"
                placeholder="请选择时间"
                @on-change="formModel.fields.finishTime=$event"
                :value="formModel.fields.finishTime"
              ></Date-picker>
            </FormItem>
          </Col>-->
        </Row>
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="内容描述" prop="missionDescribe">
              <Input
                v-model="formModel.fields.missionDescribe"
                placeholder="请输入内容描述"
                type="textarea"
                :rows="5"
              />
            </FormItem>
          </Col>
        </Row>
        <!-- <Row :gutter="16"> -->
        <!-- <Col span="12">
            <FormItem label="负责人" prop="selectfuzename">
              <Input
                v-model="formModel.fields.selectfuzename"
                @on-change="selectfuze"
                style="width: 150px;"
                placeholder="请选择负责人"
                :readonly="true"
              />
              <i-button type="info" @click="selectfuze(rowid)">选择负责人</i-button>
              <Select v-model="formModel.fields.principal" placeholder="负责人" filterable>
                <Option v-for="item in fuzerendata" :value="item.systemUserUUID">{{item.realName}}</Option>
        </Select>-->
        <!-- </FormItem>
        </Col>-->
        <!-- <Col span="12">
            <FormItem label="参与人" prop="selectcanyurenname">
              <Input
                v-model="formModel.fields.selectcanyurenname"
                @on-change="selectcanyu"
                style="width: 150px;"
                placeholder="请选择参与人"
                :readonly="true"
              />
              <Button icon="md-checkmark-circle" type="ghost" @click="selectcanyu">选择参与人</Button>
              <i-button type="info" @click="selectcanyu(rowid)">选择参与人</i-button>
            </FormItem>
          </Col>
        </Row>-->
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="原附件" v-show="shows">
              <Input v-model="formModel.fields.accessory" :readonly="true" />
            </FormItem>
            <FormItem label="选择新附件" prop="photograph">
              <input
                style="cursor:default;"
                class="ivu-input"
                type="file"
                @change="addImg1"
                ref="inputer1"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <!-- <Col span="12">
            <FormItem label="审批人" prop="selectshenpiname">

					<Input v-model="formModel.fields.selectshenpiname" @on-change="selectshenpi" style="width: 150px;" placeholder="请选择审批人" :readonly="true"/>
					<i-button type="info" @click="selectshenpi(rowid)">选择审批人</i-button>

            </FormItem>
          </Col>-->
          <Col span="16">
            <FormItem label="创建人" prop="establishName">
              <Input v-model="formModel.fields.establishName" :readonly="true" placeholder="创建人" />
            </FormItem>
          </Col>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmit">提 交</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <!-- 查看的表单 -->
    <Drawer
      title="查看任务"
      v-model="formModel2.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form
        :model="formModel2.fields"
        ref="formtask2"
        :rules="formModel2.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="科室" prop="priorityname">
              <Input
                v-model="formModel2.fields.priorityname"
                :title="formModel2.fields.priorityname"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="标题" prop="missionHeadline">
              <Input
                v-model="formModel2.fields.missionHeadline"
                :title="formModel2.fields.missionHeadline"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
        </Row>
        <!-- <Row :gutter="16">
          <Col span="12">
            <FormItem label="负责人" prop="selectfuzename">
              <Input
                v-model="formModel2.fields.selectfuzename"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="参与人" prop="selectcanyurenname">
              <Input
                v-model="formModel2.fields.selectcanyurenname"
                :title="formModel2.fields.selectcanyurenname"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
        </Row>-->
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="撰写时间" prop="startTime">
              <Input v-model="formModel2.fields.startTime" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
          <!-- <Col span="12">
            <FormItem label="结束时间" prop="finishTime">
              <Input v-model="formModel2.fields.finishTime" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>-->
        </Row>
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="任务描述" prop="missionDescribe">
              <Input
                v-model="formModel2.fields.missionDescribe"
                placeholder="请输入任务描述"
                type="textarea"
                :rows="5"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>

        <!-- <Row :gutter="16">
          <Col span="12">
            <FormItem label="审批人" prop="selectshenpiname">
              <Input
                v-model="formModel2.fields.selectshenpiname"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="状态" prop="auditStatus">
              <Input v-model="formModel2.fields.auditStatus" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
        </Row>-->
        <!-- <Row :gutter="16" v-if="beizhushow">
          <Col span="50">
            <FormItem label="备注" prop="remark">
              <Input
                v-model="formModel2.fields.remark"
                placeholder="备注"
                type="textarea"
                :rows="5"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>-->
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="附件" prop="accessory">
              <Button
                icon="ios-cloud-download-outline"
                shape="circle"
                size="small"
                type="primary"
                @click="handleimportmodel"
                title="下载"
                下载
              ></Button>
              <Input v-model="formModel2.fields.accessory" :readonly="true" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="撰写时间" prop="establishTime">
              <Input
                v-model="formModel2.fields.establishTime"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="创建人" prop="establishName">
              <Input
                v-model="formModel2.fields.establishName"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
        </Row>
        <!-- 查看使用的审核意见 -->
        <!-- <Row :gutter="16" v-if="yijianshow">
          <Col span="50">
            <FormItem label="审核意见" prop="auditOpinion">
              <Input
                v-model="formModel2.fields.auditOpinion"
                placeholder="审核意见"
                type="textarea"
                :rows="5"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>-->
        <!-- 审核用的审核意见 -->
        <!-- <Row :gutter="16" v-if="shenhea">
          <Col span="50">
            <FormItem label="审核意见" prop="auditOpinion">
              <Input
                v-model="formModel2.fields.auditOpinion"
                placeholder="审核意见"
                type="textarea"
                :rows="5"
              />
            </FormItem>
          </Col>
        </Row>-->
      </Form>
      <div class="demo-drawer-footer" v-if="shenhea">
        <Button
          icon="md-checkmark-circle"
          type="primary"
          style="margin-right: 10px;"
          @click="handok"
        >通 过</Button>
        <Button type="error" icon="md-close" @click="handononon">退 回</Button>
      </div>
    </Drawer>

    <!-- 完成的表单 -->
    <Drawer
      title="完成任务"
      v-model="formModel3.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form
        :model="formModel3.fields"
        ref="formtask3"
        :rules="formModel3.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="科室" prop="priorityname">
              <Input v-model="formModel3.fields.priorityname" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="标题" prop="missionHeadline">
              <Input
                v-model="formModel3.fields.missionHeadline"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="负责人" prop="selectfuzename">
              <Input
                v-model="formModel3.fields.selectfuzename"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="参与人" prop="selectcanyurenname">
              <Input
                v-model="formModel3.fields.selectcanyurenname"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="撰写时间" prop="startTime">
              <Input v-model="formModel3.fields.startTime" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="结束时间" prop="finishTime">
              <Input v-model="formModel3.fields.finishTime" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="任务描述" prop="missionDescribe">
              <Input
                v-model="formModel3.fields.missionDescribe"
                placeholder="请输入任务描述"
                type="textarea"
                :rows="5"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="审批人" prop="selectshenpiname">
              <Input
                v-model="formModel3.fields.selectshenpiname"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="创建人" prop="establishName">
              <Input
                v-model="formModel3.fields.establishName"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="备注" prop="remark">
              <Input v-model="formModel3.fields.remark" placeholder="备注" type="textarea" :rows="5" />
            </FormItem>
          </Col>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handwancheng">提 交</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel3.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer
      title="选择审批人"
      v-model="formModelshenpi.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelshenpi.fields" ref="formselect" label-position="top">
        <FormItem label="所属科室" prop="uuid">
          <Select
            v-model="formMode4.fields.uuid"
            @on-change="binddata(formMode4.fields.uuid)"
            placeholder="所属科室"
            filterable
          >
            <Option
              v-for="item in keshidatalist"
              :value="item.priorityUUID"
              :title="item.priorityHeadline"
            >{{item.priorityHeadline}}</Option>
          </Select>
        </FormItem>

        <FormItem label="审批人" prop="fuze">
          <Transfer
            :data="formModelshenpi.fields.shenpileft"
            :target-keys="formModelshenpi.fields.shenpiright"
            :list-style="{width: '240px',height: '400px'}"
            :titles="['所有人员','选择的人员']"
            filterable
            @on-change="handleSchoolChange3"
          ></Transfer>
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="formModelshenpi.opened = false"
        >确 定</Button>
      </div>
    </Drawer>

    <Drawer
      title="选择负责人"
      v-model="formModelfuze.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelfuze.fields" ref="formselect" label-position="top">
        <FormItem label="所属科室" prop="uuid">
          <Select
            v-model="formMode4.fields.uuid"
            @on-change="binddata(formMode4.fields.uuid)"
            placeholder="所属科室"
            filterable
          >
            <Option
              v-for="item in keshidatalist"
              :value="item.priorityUUID"
              :title="item.priorityHeadline"
            >{{item.priorityHeadline}}</Option>
          </Select>
        </FormItem>
        <FormItem label="负责人" prop="fuze">
          <Transfer
            :data="formModelfuze.fields.fuzeleft"
            :target-keys="formModelfuze.fields.fuzeright"
            :list-style="{width: '240px',height: '400px'}"
            :titles="['所有人员','选择的人员']"
            filterable
            @on-change="handleSchoolChange2"
          ></Transfer>
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelfuze.opened = false">确 定</Button>
      </div>
    </Drawer>

    <Drawer
      title="选择参与人"
      v-model="formModelcanyu.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelcanyu.fields" ref="formselect" label-position="top">
        <FormItem label="所属科室" prop="uuid">
          <Select
            v-model="formMode4.fields.uuid"
            @on-change="binddataid(formMode4.fields.uuid)"
            placeholder="所属科室"
            filterable
          >
            <Option
              v-for="item in keshidatalist"
              :value="item.priorityUUID"
              :title="item.priorityHeadline"
            >{{item.priorityHeadline}}</Option>
          </Select>
        </FormItem>
        <FormItem label="参与人" prop="participant">
          <Transfer
            :data="formModelcanyu.fields.participantleft"
            :target-keys="formModelcanyu.fields.participantright"
            :list-style="{width: '240px',height: '400px'}"
            :titles="['所有人员','选择的人员']"
            filterable
            @on-change="handleSchoolChange"
          ></Transfer>
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelcanyu.opened = false">确 定</Button>
      </div>
    </Drawer>
  </div>
</template>

<script>
import DzTable from "_c/tables/dz-table.vue";
import config from "@/config";
import {
  keshizongjie, //获取所有数据
  wanchen,
  editpower,
} from "@/api/taskpai/wofuzetask";
import {
  RegistPicture, //上传文件
} from "@/api/Personal/personaldiary";
import { shenhe, shenheontongguo } from "@/api/taskpai/woshepitask";
import {
  getpersonaldiary,
  getfuzeren,
  getcanyuren,
  getcanyuren2,
  Editkeshi, //查看详情-编辑的数据
  baocunEditkeshi, //编辑-保存
  binddataok,
  binddataok2,
  createkeshi, //创建科室总结
  deletelist, //删除单行数据
  appcreaterizhi,
  caozuolist,
  keshidata,
  systemuserlistuuid,
  systemuserlistid,
} from "@/api/taskpai/taskapis2";

export default {
  url: "",
  name: "rbac_user_page",
  components: {
    DzTable,
  },
  data() {
    return {
      commands: {
        delete: { name: "delete", title: "删除" },
      },
      options3: {
        disabledDate(date) {
          return date && date.valueOf() < Date.now() - 86400000;
        },
      },
      rowuuid: "",
      chatTimer: null,
      //操作日志
      formmodelrizhi: {
        missionUuid: "", //所属任务
        content: "", //日志内容
        establishName: "", //创建人
      },
      shows: true, //是否显示新附件
      shenhea: false,
      rizhidata: [],
      panduanrowid: 0,
      yijianshow: false, //审核意见显示
      beizhushow: false, //备注显示
      prioritydata: [], //重点工作
      keshidata: [], //科室
      fuzerendata: [], //负责人
      approver: [], //审批人
      rowid: "", //选择行的id
      accessoryas: "", //下载文件名
      formModel: {
        opened: false,
        title: "创建任务",
        mode: "create",
        selection: [],
        fields: {
          missionUUID: "",
          administrativeOffice: "", //科室
          //priorityUUID: "",//所属重点工作
          missionHeadline: "", //任务标题
          principal: "", //负责人
          selectfuzename: "", //负责人
          startTime: "", //开始时间
          finishTime: "", //结束时间
          missionDescribe: "", //任务描述
          priority: "", //优先级
          manhour: 1, //计划工时
          approver: "", //审批人
          selectshenpiname: "", //审批人
          participant: "", //选择的参与人id
          selectcanyurenname: "", //选择的参与人姓名
          establishName: "", //创建人uuid
          establisthtruename: "", //创建人姓名
          establishTime: "", //创建时间
          auditStatus: "", //审核状态
          accessory: "", //附件名
        },
        rules: {
          missionHeadline: [
            { type: "string", required: true, message: "请输入标题" },
          ],
          administrativeOffice: [
            { type: "string", required: true, message: "请选择科室" },
          ],
          missionDescribe: [
            { type: "string", required: true, message: "请输入内容" },
          ],
          priority: [
            { type: "string", required: true, message: "请选择优先级" },
          ],
          startTime: [
            { type: "string", required: true, message: "请选择撰写时间" },
          ],
          finishTime: [
            { type: "string", required: true, message: "请选择结束时间" },
          ],
          selectcanyurenname: [
            { type: "string", required: true, message: "请选择参与人" },
          ],
          //approver:[{type: "string", required: true, message: "请选择审批人" }]
          /* priorityUUID: [
            { type: "string", required: true, message: "请选择所属重点工作" }
          ], */
        },
      },
      editpowerguid: "", //返回的部门Guid
      formModel2: {
        opened: false,
        title: "查看任务",
        mode: "create",
        selection: [],
        fields: {
          missionUUID: "",
          priorityUUID: "", //所属重点工作
          missionHeadline: "", //任务标题
          principal: "", //负责人
          selectfuzename: "", //负责人
          startTime: "", //创建时间
          finishTime: "", //结束时间
          missionDescribe: "", //任务描述
          priority: "", //优先级
          manhourt: "", //计划工时
          approver: "", //审批人
          selectshenpiname: "", //审批人
          participant: "", //选择的参与人id
          selectcanyurenname: "", //选择的参与人姓名
          establishName: "", //创建人uuid
          establisthtruename: "", //创建人姓名
          establishTime: "", //创建时间
          auditOpinion: "", //审核意见
          accessory: "", //附件
        },
        rules: {
          auditOpinion: [
            { type: "string", required: true, message: "请填写审核意见" },
          ],
          missionHeadline: [
            { type: "string", required: true, message: "请输入标题" },
          ],
        },
      },
      formModel3: {
        opened: false,
        title: "完成任务",
        mode: "create",
        selection: [],
        fields: {
          missionUUID: "",
          priorityUUID: "", //所属重点工作
          missionHeadline: "", //任务标题
          principal: "", //负责人
          selectfuzename: "", //负责人
          startTime: "", //开始时间
          finishTime: "", //结束时间
          missionDescribe: "", //任务描述
          priority: "", //优先级
          manhourt: "", //计划工时
          approver: "", //审批人
          selectshenpiname: "", //审批人
          participant: "", //选择的参与人id
          selectcanyurenname: "", //选择的参与人姓名
          establishName: "", //创建人uuid
          establisthtruename: "", //创建人姓名
          establishTime: "", //创建时间
          remark: "", //备注
        },
      },
      formModelcanyu: {
        opened: false,
        title: "选择人员",
        mode: "select",
        fields: {
          participantleft: [],
          participantright: [],
        },
      },

      formModelfuze: {
        opened: false,
        title: "选择人员",
        mode: "select",
        fields: {
          fuzeleft: [],
          fuzeright: [],
        },
      },
      formModelshenpi: {
        opened: false,
        title: "选择人员",
        mode: "select",
        fields: {
          shenpileft: [],
          shenpiright: [],
        },
      },

      formMode4: {
        fields: {
          id: "",
          uuid: "",
          name: "",
        },
      },
      keshidatalist: [],

      stores: {
        vote: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            userguid: "",
            kwbt: "",
            kwfzr: "",
            kwendtime: "",
            isDeleted: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID",
              },
            ],
          },
          data: [],
        },
        columns: [
          { type: "selection", width: 50, key: "handle" },
          { title: "标题", key: "missionHeadline", ellipsis: true },
          { title: "内容", key: "priorityHeadline", ellipsis: true },
          { title: "部门", key: "auditStatus", ellipsis: true },
          // { title: "状态", key: "auditStatus",slot:"auditStatus"},
          // { title: "内容", key: "principal",ellipsis: true},
          {
            title: "撰写时间",
            ellipsis: true,
            tooltip: true,
            key: "startTime",
          },
          // { title: "结束时间", key: "finishTime" },
          {
            title: "操作",
            align: "left",
            width: 150,
            className: "table-command-column",
            slot: "action",
          },
        ],
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static",
      },
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "创建任务";
      }
      if (this.formModel.mode === "edit") {
        return "编辑任务";
      }
      return "";
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.id);
    },
  },
  methods: {
    //文件上传
    addImg1(event) {
      this.imgname = ""; //清除选择的文件
      let inputDOM = this.$refs.inputer1;
      let formData = new FormData();
      let file = inputDOM.files[0];
      let extName = file.name
        .substring(file.name.lastIndexOf("."))
        .toLowerCase();
      if (extName == ".exe" || extName == ".EXE") {
        this.$Message.warning("文件格式不正确!");
        return false;
      }
      // 通过DOM取文件数据
      this.fil = inputDOM.files;
      for (let i = 0; i < this.fil.length; i++) {
        let size = Math.floor(this.fil[i].size / 1024);
        if (size > 2 * 1024 * 1024) {
          this.$Message.warning("请选择2M以内的文件！");
          return false;
        }
        formData.append("multipartFiles", this.fil[i], this.fil[i].name);
        RegistPicture(formData).then((res) => {
          if (res.data.code == "200") {
            this.$Message.success(res.data.msg);
            this.img1 = this.url + res.data.path;
            this.imgshow1 = true;
            this.imgcopy1 = res.data.path;
            this.imgname = res.data.path;
            this.formModel.fields.accessory = res.data.path;
          } else {
            this.$Message.warning(res.data.msg);
          }
        });
      }
    },
    //下载附件
    handleimportmodel() {
      window.location.href =
        this.url + "UploadFiles/PersonalDiary/" + this.accessoryas;
    },
    //获取调度数据
    loadVoteinitiateList() {
      this.stores.vote.query.userguid = this.$store.state.user.userGuid; //当前登录账号guid
      //科室工作总结
      keshizongjie(this.stores.vote.query).then((res) => {
        this.stores.vote.data = res.data.data;
        this.stores.vote.query.totalCount = res.data.totalCount;
      });
    },
    //***************************************************************************
    handleSortChange(column) {
      this.stores.query.sort.direction = column.order;
      this.stores.query.sort.field = column.key;
      this.loadVoteinitiateList();
    },
    //刷新
    handleRefresh() {
      this.loadVoteinitiateList();
    },
    //获取选择的行数据
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.vote.query.currentPage = page;
      this.loadVoteinitiateList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.vote.query.pageSize = pageSize;
      this.loadVoteinitiateList();
    },
    //搜索-查询
    handleSearch() {
      console.log(this.stores.vote.query);
      this.loadVoteinitiateList();
    },
    //点击添加打开窗口
    handleShowCreateWindow() {
      debugger;
      this.$refs.inputer1.value = "";//清除附件名
      console.log(this.formModelfuze.fields.fuzeright);
      console.log(this.formModelfuze.fields.fuzeleft);
      this.shows = false; //隐藏新附件样式
      this.formModel.mode = "create"; //添加
      this.formModel.opened = true; //打开窗口
      this.$refs["formtask"].resetFields(); //清空填写的表单信息
      this.formModelfuze.fields.fuzeright = []; //重新打开窗口，清空选择的人员信息负责人

      this.formModelshenpi.fields.shenpiright = []; //重新打开窗口，清空选择的人员信息审批人
      this.formModelcanyu.fields.participantright = []; //重新打开窗口，清空选择的人员信息 参与人
      this.formModel.fields.establishName = this.$store.state.user.userName; //创建人
      var date = new Date();
      var seperator1 = "-";
      var seperator2 = ":";
      var seperator3 = " ";
      var year = date.getFullYear();
      var month = date.getMonth() + 1;
      var strDate = date.getDate();
      var hourss = date.getHours();
      var fenzhong = date.getMinutes();
      var miao = date.getSeconds();
      if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
      }
      if (month >= 0 && month <= 9) {
        month = "0" + month;
      }
      if (hourss >= 0 && hourss <= 9) {
        hourss = "0" + hourss;
      }
      if (fenzhong >= 0 && fenzhong <= 9) {
        fenzhong = "0" + fenzhong;
      }
      if (miao >= 0 && miao <= 9) {
        miao = "0" + miao;
      }
      //拼接的当前时间
      var currentdateTime =
        year +
        seperator1 +
        month +
        seperator1 +
        strDate +
        seperator3 +
        hourss +
        seperator2 +
        fenzhong +
        seperator2 +
        miao;
      var currentdate = month + "月科室总结";
      this.formModel.fields.missionHeadline = currentdate;
      this.formModel.fields.startTime = currentdateTime;
    },
    //点击编辑打开窗口
    handleEdit(row) {
      this.ceshi();
      if (row.missionID == this.editpowerguid) {
        console.log("askjdchaischi嘉士伯");
        console.log(row);
        this.shows = true;
        this.formModel.mode = "edit"; //修改
        this.formModel.opened = true;
        this.rowid = row.id; //保存选择行的id
        this.formModelfuze.fields.fuzeright = []; //重新打开窗口，清空选择的人员信息负责人
        this.formModelshenpi.fields.shenpiright = []; //重新打开窗口，清空选择的人员信息审批人
        this.formModelcanyu.fields.participantright = []; //重新打开窗口，清空选择的人员信息 参与人
        Editkeshi({ id: row.id }).then((res) => {
          this.formModel.fields = res.data.data;
        });
      } else {
        this.$Message.warning("非本科室人员！");
      }
    },
    //重点工作、科室下拉框
    loadpersonaldiary() {
      getpersonaldiary().then((res) => {
        this.prioritydata = res.data.data;
      });
      keshidata().then((res) => {
        this.keshidata = res.data.data;
      });
    },
    //负责人和审批人下拉框
    loadfuzeren() {
      getfuzeren().then((res) => {
        this.approver = this.fuzerendata = res.data.data;
      });
    },
    //穿梭框负责人 审批人
    loaselect2() {
      getcanyuren2().then((res) => {
        this.formModelshenpi.fields.shenpileft = this.formModelfuze.fields.fuzeleft =
          res.data.data;
      });
    },
    //穿梭框
    loaselect() {
      getcanyuren({ id: 0 }).then((res) => {
        this.formModelcanyu.fields.participantleft = res.data.data;
      });
    },

    //打开选择审批人窗口
    selectshenpi(rowid) {
      this.formModelshenpi.opened = true;
      //修改
      if (rowid != "") {
        console.log(rowid);
        binddataok2({ id: rowid }).then((res) => {
          if (this.panduanrowid != rowid) {
            //判断是否是本行的数据
            //不是本行数据
            this.formModelshenpi.fields.shenpiright = []; //清除上次选择的人员
          }
          //如果没有选择人员则显示数据库中的内容（表示选择了新行）
          if (this.formModelshenpi.fields.shenpiright.length == 0) {
            if (res.data.data.length > 0) {
              for (let i = 0; i < res.data.data.length; i++) {
                //显示数据库保存的人员
                this.formModelshenpi.fields.shenpiright = this.formModelshenpi.fields.shenpiright.concat(
                  res.data.data[i].key
                );
              }
            }
          }
          this.panduanrowid = rowid; //将本行的id保存
        });
      }
    },
    //选择审批人
    handleSchoolChange3(newTargetKeys, direction, moveKeys) {
      this.formModelshenpi.fields.shenpiright = newTargetKeys;
      this.formModel.fields.selectshenpiname = this.formModel.fields.approver =
        ""; //每次选择则负责人都重新赋值
      for (let i = 0; i < this.formModelshenpi.fields.shenpiright.length; i++) {
        for (
          let j = 0;
          j < this.formModelshenpi.fields.shenpileft.length;
          j++
        ) {
          if (
            this.formModelshenpi.fields.shenpiright[i] ===
            this.formModelshenpi.fields.shenpileft[j].key
          ) {
            this.formModel.fields.selectshenpiname +=
              this.formModelshenpi.fields.shenpileft[j].label + ",";
            this.formModel.fields.approver +=
              this.formModelshenpi.fields.shenpileft[j].key + ",";
          }
        }
      }
    },

    //打开选择负责人窗口
    selectfuze(rowid) {
      this.formModelfuze.opened = true;
      //修改
      if (rowid != "") {
        console.log(rowid);
        binddataok2({ id: rowid }).then((res) => {
          if (this.panduanrowid != rowid) {
            //判断是否是本行的数据
            //不是本行数据
            this.formModelfuze.fields.fuzeright = []; //清除上次选择的人员
          }
          //如果没有选择人员则显示数据库中的内容（表示选择了新行）
          if (this.formModelfuze.fields.fuzeright.length == 0) {
            if (res.data.data.length > 0) {
              for (let i = 0; i < res.data.data.length; i++) {
                //显示数据库保存的人员
                this.formModelfuze.fields.fuzeright = this.formModelfuze.fields.fuzeright.concat(
                  res.data.data[i].key
                );
              }
            }
          }
          this.panduanrowid = rowid; //将本行的id保存
        });
      } else {
        this.formModelfuze.fields.fuzeright = []; //清除上次选择的人员
      }
    },
    //选择负责人
    handleSchoolChange2(newTargetKeys, direction, moveKeys) {
      this.formModelfuze.fields.fuzeright = newTargetKeys;
      this.formModel.fields.selectfuzename = this.formModel.fields.principal =
        ""; //每次选择则负责人都重新赋值
      for (let i = 0; i < this.formModelfuze.fields.fuzeright.length; i++) {
        for (let j = 0; j < this.formModelfuze.fields.fuzeleft.length; j++) {
          if (
            this.formModelfuze.fields.fuzeright[i] ===
            this.formModelfuze.fields.fuzeleft[j].key
          ) {
            this.formModel.fields.selectfuzename +=
              this.formModelfuze.fields.fuzeleft[j].label + ",";
            this.formModel.fields.principal +=
              this.formModelfuze.fields.fuzeleft[j].key + ",";
          }
        }
      }
    },

    //打开选择参与人窗口
    selectcanyu(rowid) {
      this.formModelcanyu.opened = true;
      //修改
      if (rowid != "") {
        binddataok({ id: rowid }).then((res) => {
          if (this.panduanrowid != rowid) {
            //判断是否是本行的数据
            //不是本行数据
            this.formModelcanyu.fields.participantright = []; //清除上次选择的人员
          }
          //如果没有选择人员则显示数据库中的内容（表示选择了新行）
          if (this.formModelcanyu.fields.participantright.length == 0) {
            if (res.data.data.length > 0) {
              for (let i = 0; i < res.data.data.length; i++) {
                //显示数据库保存的人员
                this.formModelcanyu.fields.participantright = this.formModelcanyu.fields.participantright.concat(
                  res.data.data[i].key
                );
              }
            }
          }
          this.panduanrowid = rowid; //将本行的id保存
        });
      }
    },
    //选择参与人
    handleSchoolChange(newTargetKeys, direction, moveKeys) {
      this.formModelcanyu.fields.participantright = newTargetKeys;
      this.formModel.fields.selectcanyurenname = this.formModel.fields.participant =
        ""; //每次选择则参与人都重新赋值
      for (
        let i = 0;
        i < this.formModelcanyu.fields.participantright.length;
        i++
      ) {
        for (
          let j = 0;
          j < this.formModelcanyu.fields.participantleft.length;
          j++
        ) {
          if (
            this.formModelcanyu.fields.participantright[i] ===
            this.formModelcanyu.fields.participantleft[j].key
          ) {
            this.formModel.fields.selectcanyurenname +=
              this.formModelcanyu.fields.participantleft[j].label + ",";
            this.formModel.fields.participant +=
              this.formModelcanyu.fields.participantleft[j].key + ",";
          }
        }
      }
      /* 		  console.log("选择的人员")
		  console.log(this.selectcanyurenname) */
    },
    //点击保存-创建、编辑
    handleSubmit() {
      let valid = this.validateForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          //保存添加的内容
          this.formModel.fields.establisthtruename = this.$store.state.user.userGuid; //当前登录账号guid(数据库保存创建人UUID)
          createkeshi(this.formModel.fields).then((res) => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.formModel.opened = false;
              this.loadVoteinitiateList();
            } else {
              this.$Message.warning(res.data.message);
            }
          });
        }
        if (this.formModel.mode === "edit") {
          //保存修改的内容
          baocunEditkeshi(this.formModel.fields).then((res) => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.formModel.opened = false;
              this.loadVoteinitiateList();
            } else {
              this.$Message.warning(res.data.message);
            }
          });
        }
      }
    },
    //验证表单信息
    validateForm() {
      let _valid = false;
      this.$refs["formtask"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善表单信息！");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    validateForm2() {
      let _valid = false;
      this.$refs["formtask2"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },

    //删除多条数据
    handleBatchCommand(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content: "<p>您确定要删除吗?</p>",
        loading: true,
        onOk: () => {
          /* 			  console.log("删除选择的")
	  		console.log(this.selectedRowsId.join(",")) */
          deletelist({ ids: this.selectedRowsId.join(",") }).then((res) => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.loadVoteinitiateList();
              this.$Modal.remove(); //关闭提示框
            } else {
              this.$Message.warning(res.data.message);
            }
          });
        },
      });
    },
    //删除单条数据
    handleDelete(row) {
      /* console.log(row.id) */
      deletelist({ ids: row.id }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadVoteinitiateList();
          $Message.success("操作成功");
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //查看
    lookEwq(row) {
      this.shenhea = false;
      this.yijianshow = this.beizhushow = false;
      this.formModel2.opened = true;
      Editkeshi({ id: row.id }).then((res) => {
        this.formModel2.fields = res.data.data;
        this.accessoryas = this.formModel2.fields.accessory;
        console.log("测试数据@");
        console.log(this.accessoryas);
        if (row.auditStatus != "0") {
          //已提交完成则显示备注
          this.beizhushow = true;
        }

        if (
          res.data.data.auditOpinion != "" &&
          res.data.data.auditOpinion != null
        ) {
          //已完成任务则显示审核信息
          this.yijianshow = true;
        }
      });
    },

    //选择科室重新绑定人员信息
    binddata(data) {
      systemuserlistuuid({ uuid: data }).then((res) => {
        this.formModelshenpi.fields.shenpileft = this.formModelfuze.fields.fuzeleft =
          res.data.data;
      });
    },
    binddataid(data) {
      systemuserlistid({ uuid: data }).then((res) => {
        console.log(res);
        this.formModelcanyu.fields.participantleft = res.data.data;
      });
    },
    keshijiazai() {
      keshidata().then((res) => {
        this.keshidatalist = this.keshidatalistid = res.data.data;
        console.log("科室数据");
        console.log(this.keshidatalist);
      });
    },

    //完成
    wancheng(row) {
      this.formModel3.opened = true;
      Editkeshi({ id: row.id }).then((res) => {
        this.formModel3.fields = res.data.data;
      });
    },
    //完成任务提交
    handwancheng() {
      this.$Modal.confirm({
        title: "操作提示",
        content: "<p>此操作将完成该任务,是否继续?</p>",
        loading: true,
        onOk: () => {
          wanchen(this.formModel3.fields).then((res) => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.loadVoteinitiateList();
              this.$Modal.remove(); //关闭提示框
            } else {
              this.$Message.warning(res.data.message);
            }
            this.formModel3.opened = false; //关闭窗口
          });
        },
      });
    },

    shenpishow(row) {
      var show = false;

      if (
        row.auditStatus == "1" &&
        row.approver.search(this.$store.state.user.userGuid) != -1
      ) {
        //审批中的任务并且当前登录人为此任务审批人可以进行审批
        show = true;
      }
      return show;
    },
    //审批
    shenpitask(row) {
      this.formModel2.opened = true;
      this.shenhea = true; //审核则显示审核按钮
      Editkeshi({ id: row.id }).then((res) => {
        this.formModel2.fields = res.data.data;
      });
    },
    //审核通过
    handok() {
      let valid = this.validateForm2();
      if (valid) {
        this.$Modal.confirm({
          title: "操作提示",
          content: "<p>确定审核通过?</p>",
          loading: true,
          onOk: () => {
            shenhe(this.formModel2.fields).then((res) => {
              if (res.data.code === 200) {
                this.$Message.success(res.data.message);
                this.loadVoteinitiateList();
                this.$Modal.remove(); //关闭提示框
              } else {
                this.$Message.warning(res.data.message);
              }
            });
            this.formModel2.opened = false; //关闭打开的窗口
          },
        });
      }
    },
    //审核不通过
    handononon() {
      let valid = this.validateForm2();
      if (valid) {
        this.$Modal.confirm({
          title: "操作提示",
          content: "<p>确定退回此任务?</p>",
          loading: true,
          onOk: () => {
            shenheontongguo(this.formModel2.fields).then((res) => {
              if (res.data.code === 200) {
                this.$Message.success(res.data.message);
                this.loadVoteinitiateList();
                this.$Modal.remove(); //关闭提示框
              } else {
                this.$Message.warning(res.data.message);
              }
            });
            this.formModel2.opened = false; //关闭打开的窗口
          },
        });
      }
    },
    getauditStatus(auditStatus) {
      var auditStatusText = "未知";
      switch (auditStatus) {
        case "0":
          auditStatusText = "进行中";
          break;
        case "1":
          auditStatusText = "审核中";
          break;
        case "2":
          auditStatusText = "已完成";
          break;
      }
      return auditStatusText;
    },
    wanchengshow(row) {
      var show = false;

      if (
        row.auditStatus == "0" &&
        row.principaluuid.search(this.$store.state.user.userGuid) != -1
      ) {
        //进行中的任务并且当前登录人为此任务负责人可以完成
        show = true;
      }
      return show;
    },
    xiugai(row) {
      var show = false;
      if (
        (row.auditStatus == "0" &&
          row.adduser == this.$store.state.user.userGuid) ||
        row.principaluuid == this.$store.state.user.userGuid
      ) {
        //进行中的任务并且当前登录人为此任务负责人或添加人可以修改
        show = true;
      }
      return show;
    },
    ceshi() {
      var guid = this.$store.state.user.userGuid;
      editpower({ guid: guid }).then((res) => {
        console.log("行数据21313");
        console.log(res);
        this.editpowerguid = res.data.data.departmentUuid;
        console.log(this.editpowerguid);
      });
    },
  },
  mounted() {
     
    this.loadVoteinitiateList(); //加载所有数据
    this.loadpersonaldiary(); //重点工作下拉框
    this.keshijiazai(); //科室下拉框
    this.loadfuzeren(); //负责人下拉框
    this.loaselect(); //穿梭框参与人
    this.loaselect2(); //穿梭框负责人审批人
    this.url = config.baseUrl.dev;
  },
};
</script>

<style>
</style>
