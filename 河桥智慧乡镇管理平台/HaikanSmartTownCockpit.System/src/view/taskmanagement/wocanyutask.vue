<template>
	<div>
		<Card>
			<dz-table :totalCount="stores.vote.query.totalCount" :pageSize="stores.vote.query.pageSize" @on-page-change="handlePageChanged"
			 @on-page-size-change="handlePageSizeChanged">
				<div slot="searcher">
					<section class="dnc-toolbar-wrap">
						<Row :gutter="16">
							<Col span="16">
							<Form inline @submit.native.prevent>

													<FormItem>
		<Select style="width: 180px;" v-model="stores.vote.query.kwsszdgz" placeholder="所属重点工作" :clearable="true" filterable>
			<Option v-for="item in prioritydata" :key="item.priorityUUID" :value="item.priorityUUID" :title="item.priorityHeadlinelong">{{item.priorityHeadline}}</Option>
		</Select>
            </FormItem>
								<FormItem>
									<Input search type="text" style="width: 180px;" :clearable="true" v-model="stores.vote.query.kwbt" placeholder="输入任务标题搜索..."
									 @on-search="handleSearch()">
									</Input>
								</FormItem>
								<FormItem>
									<Select v-model="stores.vote.query.kwfzr" :clearable="true" style="width: 180px;" placeholder="请选择负责人" filterable>
					  	<Option v-for="item in fuzerendata" :value="item.systemUserUUID" :key="item.systemUserUUID">{{item.realName}}</Option>
					  </Select>
								</FormItem>
								<!-- <FormItem>
              <Select v-model="stores.vote.query.zt" placeholder="状态" clearable>
                <Option value="0">进行中</Option>
				<Option value="1">审核中</Option>
				<Option value="2">已完成</Option>
              </Select>
            </FormItem> -->
				<FormItem>
              <Select v-model="stores.vote.query.yxj" placeholder="请选择优先级" clearable>
                <Option value="普通">普通</Option>
				<Option value="紧急">紧急</Option>
				<Option value="非常紧急">非常紧急</Option>
              </Select>
            </FormItem>
				  <FormItem>
				  <Button
				  v-can="'search'"
				    icon="md-search"
				    type="primary"
				    @click="handleSearch()"
				    title="查询">查询
				  </Button>
				  </FormItem>
							</Form>
							</Col>
							<Col span="8" class="dnc-toolbar-btns">
							<ButtonGroup class="mr3">
								<!--                  <Button
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button> -->
								<Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
							</ButtonGroup>
							<!--                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="新增任务"
                >新增任务</Button> -->
							</Col>
						</Row>
					</section>
				</div>
				<Table slot="table" ref="tables" :border="false" size="small" :row-class-name="rowClassName" :highlight-row="true" :data="stores.vote.data"
				 :columns="stores.columns" @on-selection-change="handleSelectionChange" @on-refresh="handleRefresh" @on-page-change="handlePageChanged"
				 @on-page-size-change="handlePageSizeChanged" @on-sort-change="handleSortChange">
					<template slot-scope="{row,index}" slot="auditStatus">
						<span>{{getauditStatus(row.auditStatus)}}</span>
					</template>
					<template slot-scope="{ row, index }" slot="action">
						<!--            <Poptip
              confirm
              :transfer="true"
              title="确定将此任务放入回收站?"
              @on-ok="handleDelete(row)"
              >
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button type="error" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
              <Button v-can="'edit'" type="primary" size="small" shape="circle" icon="md-create" @click="handleEdit(row)"></Button>
            </Tooltip> -->
						<Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
							<Button v-can="'show'" type="success" size="small" shape="circle" icon="md-search" @click="lookEwq(row)"></Button>
						</Tooltip>
						<Tooltip placement="top" content="汇报" :delay="1000" v-if="huibaoshow(row)" :transfer="true">
							<Button v-can="'report'" type="primary" size="small" shape="circle" icon="md-clipboard" @click="huibao(row)"></Button>
						</Tooltip>
					</template>
				</Table>
			</dz-table>
		</Card>






<!-- 任务汇报添加 -->
		<Drawer title="添加汇报" v-model="formModelhb.opened" width="600" :mask-closable="false" :mask="true" :styles="styles">
			<Form :model="formModelhb.fields" ref="formtaskhb" :rules="formModelhb.rules" label-position="top">
				<Row :gutter="16">
					<FormItem label="任务标题" prop="missionHeadline">
						<Input v-model="formModelhb.fields.missionHeadline" :title="formModelhb.fields.missionHeadline" :readonly="true" placeholder="未知..." />
					</FormItem>

				</Row>
				<Row :gutter="16">
					
					<FormItem label="已完成" prop="completed">
						<Input v-model="formModelhb.fields.completed" placeholder="已完成" type="textarea" :rows="5" />
					</FormItem>
				</Row>
				<Row :gutter="16">
					
					<FormItem label="需要协调" prop="coordination">
						<Input v-model="formModelhb.fields.coordination" placeholder="需要协调" type="textarea" :rows="5" />
					</FormItem>
				</Row>
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="附件上传" prop="accessory">
						<input style="cursor:default;" class="ivu-input" type="file" @change="addImg1" ref="inputer1" />
					</FormItem>
						</Col>
						<Col span="12">
					<FormItem label="汇报人" prop="establishName">
						<Input v-model="formModelhb.fields.establishName" :title="formModelhb.fields.establishName" :readonly="true" placeholder="未知..." />
					</FormItem>
						</Col>
				</Row>

			</Form>
			<div class="demo-drawer-footer">
				<Button icon="md-checkmark-circle" type="primary" @click="tijiao">提 交</Button>
				<Button style="margin-left: 8px" icon="md-close" @click="formModelhb.opened = false">取 消</Button>
			</div>
		</Drawer>


		<!-- 添加、修改的表单 -->
		<Drawer :title="formTitle" v-model="formModel.opened" width="600" :mask-closable="false" :mask="true" :styles="styles">
			<Form :model="formModel.fields" ref="formtask" :rules="formModel.rules" label-position="top">
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="所属重点工作" prop="priorityUUID">
						<Select v-model="formModel.fields.priorityUUID" placeholder="所属重点工作">
							<Option v-for="item in prioritydata" :value="item.priorityUUID" :title="item.priorityHeadlinelong">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="任务标题" prop="missionHeadline">
						<Input v-model="formModel.fields.missionHeadline" placeholder="请输入任务标题" />
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="负责人" prop="principal">
						<Select v-model="formModel.fields.principal" placeholder="负责人">
							<Option v-for="item in fuzerendata" :value="item.systemUserUUID">{{item.realName}}</Option>
						</Select>
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="优先级" prop="priority">
						<Select v-model="formModel.fields.priority">
							<Option value="普通">普通</Option>
							<Option value="紧急">紧急</Option>
							<Option value="非常紧急">非常紧急</Option>
						</Select>
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="开始时间" prop="startTime">
						<Date-picker type="datetime" format="yyyy-MM-dd HH:mm" placeholder="请选择时间" @on-change="formModel.fields.startTime=$event"
						 :value="formModel.fields.startTime"></Date-picker>
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="结束时间" prop="finishTime">
						<Date-picker type="datetime" format="yyyy-MM-dd HH:mm" placeholder="请选择时间" @on-change="formModel.fields.finishTime=$event"
						 :value="formModel.fields.finishTime"></Date-picker>
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<Col span="50">
					<FormItem label="任务描述" prop="missionDescribe">
						<Input v-model="formModel.fields.missionDescribe" placeholder="请输入任务描述" type="textarea" :rows="5" />
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="计划工时" prop="manhour">
						<Input-number :max="999999" :min="1" v-model="formModel.fields.manhour"></Input-number> 天
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="参与人" prop="selectcanyurenname">
						<Input v-model="formModel.fields.selectcanyurenname" @on-change="selectcanyu" style="width: 150px;" placeholder="请选择参与人"
						 :readonly="true" />
						<!-- <Button icon="md-checkmark-circle" type="ghost" @click="selectcanyu">选择参与人</Button> -->
						<i-button type="info" @click="selectcanyu(rowid)">选择参与人</i-button>
					</FormItem>
					</Col>
				</Row>

				<Row :gutter="16">
					<!-- <Col span="12">
					<FormItem label="审批人" prop="approver">
						<Select v-model="formModel.fields.approver" placeholder="审批人">
							<Option v-for="item in approver" :value="item.systemUserUUID">{{item.realName}}</Option>
						</Select>
					</FormItem>
					</Col> -->
					<Col span="12">
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
		<Drawer title="查看任务" v-model="formModel2.opened" :width="model2with" :mask-closable="false" :mask="true" :styles="styles">
			<Form :model="formModel2.fields" ref="formtask" :rules="formModel2.rules" v-if="lookshow" label-position="top">
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="所属重点工作" prop="priorityname">
						<Input v-model="formModel2.fields.priorityname" :title="formModel2.fields.priorityname" :readonly="true"
						 placeholder="未知..." />
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="任务标题" prop="missionHeadline">
						<Input v-model="formModel2.fields.missionHeadline" :title="formModel2.fields.missionHeadline" :readonly="true"
						 placeholder="未知..." />
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="负责人" prop="principalname">
						<Input v-model="formModel2.fields.selectfuzename" :title="formModel2.fields.selectfuzename" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="优先级" prop="priority">
						<Input v-model="formModel2.fields.priority" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<Col span="12">
					<FormItem label="开始时间" prop="startTime">
						<Input v-model="formModel2.fields.startTime" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="结束时间" prop="finishTime">
						<Input v-model="formModel2.fields.finishTime" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<Col span="50">
					<FormItem label="任务描述" prop="missionDescribe">
						<Input v-model="formModel2.fields.missionDescribe" placeholder="请输入任务描述" type="textarea" :rows="5" :readonly="true" />
					</FormItem>
					</Col>
				</Row>
				<Row :gutter="16">
					<!-- <Col span="12">
	        <FormItem label="计划工时" prop="manhourt">
	          <Input v-model="formModel2.fields.manhourt" :readonly="true" placeholder="未知..."/>
	        </FormItem>
	      </Col>-->
					<Col span="12">
					<FormItem label="参与人" prop="selectcanyurenname">
						<Input v-model="formModel2.fields.selectcanyurenname" :title="formModel2.fields.selectcanyurenname" :readonly="true"
						 placeholder="未知..." />
					</FormItem>
					</Col>
					<!-- <Col span="12">
					<FormItem label="审批人" prop="selectshenpiname">
						<Input v-model="formModel2.fields.selectshenpiname" :title="formModel2.fields.selectshenpiname" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col> -->
				</Row>

				<Row :gutter="16">
					<!-- <Col span="12">
					<FormItem label="状态" prop="auditStatus">
						<Input v-model="formModel2.fields.auditStatus" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col> -->
					<Col span="12">
					<FormItem label="创建时间" prop="establishTime">
						<Input v-model="formModel2.fields.establishTime" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col>
					<Col span="12">
					<FormItem label="创建人" prop="establishName">
						<Input v-model="formModel2.fields.establishName" :readonly="true" placeholder="未知..." />
					</FormItem>
					</Col>
				</Row>


				<Row :gutter="16" v-if="beizhushow">
					<Col span="50">
					<FormItem label="备注" prop="remark">
						<Input v-model="formModel2.fields.remark" placeholder="备注" type="textarea" :rows="5" :readonly="true" />
					</FormItem>
					</Col>
				</Row>

				<Row :gutter="16" v-if="yijianshow">
					<Col span="50">
					<FormItem label="审核意见" prop="auditOpinion">
						<Input v-model="formModel2.fields.auditOpinion" placeholder="备注" type="textarea" :rows="5" :readonly="true" />
					</FormItem>
					</Col>
				</Row>

				<!-- <label style="font-size: 15px;">操作日志：</label>
				<div id="new_message" style="max-height: 500px;overflow: auto; margin-bottom: 50px;padding-left:15px;padding-right:15px">
					<div v-for="item in rizhidata">
						<Row :gutter="16" v-if="qitashow(item.establishName)">
							<label style="font-size: 12px;margin-right: 5px;">{{item.chuanjianname}}:</label>
							<Tag style="max-width: 350px;white-space:normal; height: auto;font-size: 15px;">{{item.content}}</Tag>

							<div v-if="picshow(item.accessory)">
								<label>附件：</label>
								<label @click="handleimportmodel(item.accessory)">{{item.accessory}}</label>
							</div>
							<div v-if="!picshow(item.accessory)">
								<img class="demo-image" :src="imageList(item.accessory)" alt="暂无" style="width: 100px; height:100px" />
							</div>

							<label>{{item.establishTime}}</label>
						</Row>
						<Row :gutter="16" style="text-align: right;" v-if="dangqianshow(item.establishName)">
							<label>{{item.establishTime}}</label>
							<Tag style="font-size: 15px;max-width: 350px;white-space:normal;height: auto; background-color:lightgreen;text-align: left;">{{item.content}}</Tag>

							<div v-if="picshow(item.accessory)">
								<label>附件：</label>
								<label @click="handleimportmodel(item.accessory)">{{item.accessory}}</label>
							</div>
							<div v-if="!picshow(item.accessory)">
								<img class="demo-image" :src="imageList(item.accessory)" alt="暂无" style="width: 100px; height:100px" />
							</div>

							<label style="font-size: 12px;margin-right: 5px; color:green">{{item.chuanjianname}}</label>
						</row>
					</div>
					<Row :gutter="18" style="margin-top: 10px; width: 600px;height: 100px; background-color: white; position: fixed;bottom: 0px;">
						<input style="cursor:default;width: 70%;" class="ivu-input" type="file" ref="inputer1" />
						<Button icon="md-checkmark-circle" type="primary" @click="addImg1">上传</Button>
						<br>
						<label>请输入内容：</label>
						<Input v-model="formmodelrizhi.content" placeholder="请输入" style="width: 70%;" />
						<Button icon="md-checkmark-circle" type="primary" @click="fasong">发送</Button>
					</Row>
				</div> -->
				<i-button type="warning" @click="btnwar(formModel2.fields.missionUUID)">汇报查看</i-button>
			</Form>
				<div v-if="huibaoshows">


                    <Select
					v-model="selectdep"
                      :clearable="true"
                      style="width: 180px;"
                      placeholder="请选择部门"
                      filterable
					  @on-change="changeedep(formModel2.fields.missionUUID)"
                    >
                      <Option
                        v-for="item in deptlist"
                        :value="item.depUUID"
                        :key="item.depUUID"
                      >{{item.name}}</Option>
                    </Select>						
		
					
					<Select
					 v-model="selectuser"
                      :clearable="true"
                      style="width: 180px;"
                      placeholder="请选择人员"
                      filterable
					  @on-change="changeuser(formModel2.fields.missionUUID)"
                    >
                      <Option
                        v-for="item in userlist"
                        :value="item.systemUserUuid"
                        :key="item.systemUserUuid"
                      >{{item.realName}}</Option>
                    </Select>						
					
			

							<Table slot="table2" ref="tables2" border size="small" :data="data1"
				 :columns="columns1">
					<template slot-scope="{ row, index }" slot="actions">
						<Tooltip placement="top" content="查看详细" :delay="1000" :transfer="true">
							<i-button type="primary" size="small" @click="hblook(row)">查看</i-button>
						</Tooltip>
						<!-- <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
							<i-button type="error" size="small" >删除</i-button>
						</Tooltip> -->
					</template>
				</Table>

		

					<div>
					<i-button type="success" @click="btnsuc">返回</i-button>						
					</div>
		

				</div>
		</Drawer>




<!-- 任务汇报查看详细 -->
		<Drawer title="查看汇报" v-model="formModelhb2.opened" width="600" :mask-closable="false" :mask="true" :styles="styles">
			<Form :model="formModelhb.fields" ref="formtaskhb2" :rules="formModelhb.rules" label-position="top">
				<Row :gutter="16">
					<FormItem label="汇报人" prop="establishName">
						<Input v-model="formModelhb2.fields.establishName" :title="formModelhb2.fields.establishName" :readonly="true" placeholder="未知..." />
					</FormItem>
				</Row>
				<Row :gutter="16">
					
					<FormItem label="已完成" prop="completed">
						<Input v-model="formModelhb2.fields.completed" :title="formModelhb2.fields.completed" placeholder="已完成" :readonly="true" type="textarea" :rows="5" />
					</FormItem>
				</Row>
				<Row :gutter="16">
					
					<FormItem label="需要协调" prop="coordination">
						<Input v-model="formModelhb2.fields.coordination" :title="formModelhb2.fields.coordination" placeholder="需要协调" :readonly="true" type="textarea" :rows="5" />
					</FormItem>
				</Row>
				<Row :gutter="16">
					<FormItem label="汇报时间" prop="establishTime">
						<Input v-model="formModelhb2.fields.establishTime" :title="formModelhb2.fields.establishTime" :readonly="true" placeholder="未知..." />
					</FormItem>
				</Row>
				<Row :gutter="16">
<FormItem label="附件">
          <Button
            icon="ios-cloud-download-outline"
            shape="circle"
            size="small"
            type="primary"
            @click="handleimportmodel(formModelhb2.fields.accessory)"
            title="下载"
            下载
          ></Button>
          <Input v-model="formModelhb2.fields.accessory" :readonly="true" />

        </FormItem>
				</Row>

			</Form>
		</Drawer>







		<Drawer title="选择参与人" v-model="formModelcanyu.opened" width="600" :mask-closable="false" :mask="true" :styles="styles">
			<Form :model="formModelcanyu.fields" ref="formselect" label-position="top">

				<FormItem label="参与人" prop="participant">
					<Transfer :data="formModelcanyu.fields.participantleft" :target-keys="formModelcanyu.fields.participantright"
					 :list-style="{width: '240px',height: '400px'}" :titles="['所有人员','选择的人员']" filterable @on-change="handleSchoolChange"></Transfer>
				</FormItem>

			</Form>
			<div class="demo-drawer-footer">
				<Button icon="md-checkmark-circle" type="primary" @click="formModelcanyu.opened = false">确 定</Button>
			</div>
		</Drawer>

	</div>
</template>
<style>
    .ivu-table .demo-table-info-row td{
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-error-row td{
        /* background-color: #ff6600; */
        color: red;
    }
    .ivu-table td.demo-table-info-column{
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-name {
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-age {
        background-color: #ff6600;
        color: #fff;
    }
    .ivu-table .demo-table-info-cell-address {
        background-color: #187;
        color: #fff;
    }
</style>
<script>
	import DzTable from "_c/tables/dz-table.vue";
	import config from '@/config';
	import {
		getdatalist,
	} from "@/api/taskpai/wocanyu";
		import {
		notes,
		createhuibao,
		depdata,//根据任务绑定科室
		userdata,//绑定人员信息
		selecehuibao,//查看汇报
		hblooks,//查看汇报详细
	} from "@/api/taskpai/wofuzetask";
	import {
		getpersonaldiary,
		getfuzeren,
		getcanyuren,
		loaddata,
		baocunedit,
		binddataok,
		create,
		deletelist,
		appcreaterizhi,
		caozuolist,
		RegistPicture
	} from "@/api/taskpai/taskapis";

	export default {
		name: "rbac_user_page",
		components: {
			DzTable
		},
		data() {
			return {
				columns1: [
                    {
                        title: '姓名',
						key: 'name',
                    },
                    {
                        title: '已完成',
                        key: 'completed'
                    },
                    {
                        title: '需要协调',
                        key: 'coordination'
					},
					 {
                        title: '附件',
						key: 'accessory',
					},
					{
                        title: '添加时间',
						key: 'establishTime',
						width: 150,
					},
					{
                        title: '操作',
                        slot: "actions",
                    }
                ],
                data1: [],
				








				model2with:600,
				selectdep:"",//选择的部门
				selectuser:"",//选择的人
				userlist:[],//人员信息
				deptlist:[],
				lookshow:true,
				huibaoshows:false,



				commands: {
					delete: {
						name: "delete",
						title: "删除"
					},
				},
				chatTimer: null,
				rowuuid: "",
				//操作日志
				formmodelrizhi: {
					missionUuid: "", //所属任务
					content: "", //日志内容
					establishName: "", //创建人
					accessory: "", //附件
				},
				rizhidata: [],
				yijianshow: false, //审核意见显示
				beizhushow: false, //备注显示
				prioritydata: [], //重点工作
				fuzerendata: [], //负责人
				approver: [], //审批人
				rowid: "", //选择行的id
				formModel: {
					opened: false,
					title: "创建任务",
					mode: "create",
					selection: [],
					fields: {
						missionUUID: "",
						priorityUUID: "", //所属重点工作
						missionHeadline: "", //任务标题
						principal: "", //负责人
						startTime: "", //开始时间
						finishTime: "", //结束时间
						missionDescribe: '', //任务描述
						priority: '', //优先级
						manhour: 1, //计划工时
						approver: "", //审批人
						selectshenpiname:"",//审批人
						participant: "", //选择的参与人id	
						selectcanyurenname: "", //选择的参与人姓名
						establishName: "", //创建人uuid
						establisthtruename: "", //创建人姓名
						establishTime: "", //创建时间
						auditStatus: "", //审核状态
					},
					rules: {
						loginName: [{
							type: "string",
							required: true,
							message: "请输入登录名",
							min: 3
						}],
						realName: [],
						password: []
					}
				},
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
						startTime: "", //开始时间
						finishTime: "", //结束时间
						missionDescribe: '', //任务描述
						priority: '', //优先级
						manhourt: '', //计划工时
						approver: "", //审批人
						selectshenpiname:"",//审批人
						participant: "", //选择的参与人id
						selectcanyurenname: "", //选择的参与人姓名
						establishName: "", //创建人uuid
						establisthtruename: "", //创建人姓名
						establishTime: "", //创建时间
					},
					rules: {
						loginName: [{
							type: "string",
							required: true,
							message: "请输入登录名",
							min: 3
						}],
						realName: [],
						password: []
					}
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
						startTime: "", //开始时间
						finishTime: "", //结束时间
						missionDescribe: '', //任务描述
						priority: '', //优先级
						manhourt: '', //计划工时
						approver: "", //审批人
						selectshenpiname:"",//审批人
						participant: "", //选择的参与人id
						selectcanyurenname: "", //选择的参与人姓名
						establishName: "", //创建人uuid
						establisthtruename: "", //创建人姓名
						establishTime: "", //创建时间
						remark: "", //备注
					},
					rules: {
						loginName: [{
							type: "string",
							required: true,
							message: "请输入登录名",
							min: 3
						}],
						realName: [],
						password: []
					}
				},

				formModelhb: {
					opened: false,
					title: "任务汇报",
					mode: "create",
					selection: [],
					fields: {
						missionHeadline:"",//任务标题
						missionUUID:"",//任务uuid
						content:"",//内容
						completed:"",//已完成
						coordination:"",//需要协调
						accessory:"",//附件
						establishTime:"",//创建时间
						establishName:"",//创建人
						establisthuuid:"",//创建人uuid
						isDeleted:"",//是否删除
					},
				rules: {
						completed: [{
							type: "string",
							required: true,
							message: "请输入内容"
						}],
						coordination: [{
							type: "string",
							required: true,
							message: "请输入内容"
						}],
					}
				},
				formModelhb2: {
					opened: false,
					title: "任务汇报查看详细",
					mode: "create",
					selection: [],
					fields: {
						missionHeadline:"",//任务标题
						missionUUID:"",//任务uuid
						content:"",//内容
						completed:"",//已完成
						coordination:"",//需要协调
						accessory:"",//附件
						establishTime:"",//创建时间
						establishName:"",//创建人
						establisthuuid:"",//创建人uuid
						isDeleted:"",//是否删除
					},
				},



				formModelcanyu: {
					opened: false,
					title: "选择人员",
					mode: "select",
					fields: {
						participantleft: [],
						participantright: [],
					}
				},

				stores: {
					vote: {
						query: {
							totalCount: 0,
							pageSize: 20,
							currentPage: 1,
							kwsszdgz:"",
							userguid: "",
							kwbt: "",
							kwfzr: "",
							zt:"",
							yxj:"",
							kwendtime: "",
							isDeleted: 0,
							status: -1,
							sort: [{
								direct: "DESC",
								field: "ID"
							}]
						},
						data: []
					},
					columns: [{
							type: "selection",
							width: 50,
							key: "handle"
						},
						{
							title: "任务标题",
							key: "missionHeadline",
							ellipsis: true
						},
						{
							title: "所属重点工作",
							key: "priorityHeadline",
							ellipsis: true
						},
						{
							title: "状态",
							key: "auditStatus",
							slot: "auditStatus"
						},
						{
							title: "负责人",
							key: "principal"
						},
						{
							title: "优先级",
							key: "priority"
						},

						{
							title: "开始时间",
							ellipsis: true,
							tooltip: true,
							key: "startTime"
						},
						{
							title: "结束时间",
							key: "finishTime"
						},
						{
							title: "操作",
							align: "left",
							width: 150,
							className: "table-command-column",
							slot: "action"
						}
					]
				},
				styles: {
					height: "calc(100% - 55px)",
					overflow: "auto",
					paddingBottom: "53px",
					position: "static"
				}
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
				return this.formModel.selection.map(x => x.id);
			}
		},
		methods: {




btnwar(data){
	//部门
	depdata({uuid:data}).then(res => {
				this.deptlist=res.data.data;
			  });
			  //人员
userdata({uuid:data,depuuid:this.selectdep}).then(res=>{
	this.userlist=res.data.data;
	//汇报
	selecehuibao({uuid:data,useruuid:this.selectuser}).then(res=>{
		this.data1=res.data.data;
	})
})
this.lookshow=false;
this.huibaoshows=true;
this.model2with=800;
},

btnsuc(){
this.lookshow=true;
this.huibaoshows=false;
this.model2with=600;
},

//汇报-选择部门加载人员
changeedep(data){
userdata({uuid:data,depuuid:this.selectdep}).then(res=>{
	this.userlist=res.data.data;
})
},

//选择人员查看汇报
changeuser(data){
	selecehuibao({uuid:data,useruuid:this.selectuser}).then(res=>{
		this.data1=res.data.data;
	})
},


//添加汇报
tijiao(){
	let valid = this.validateForm2();
	if(valid){
createhuibao(this.formModelhb.fields).then(res => {
			    if (res.data.code === 200) {
			      this.$Message.success(res.data.message);
				  this.formModelhb.opened=false;
				  this.loadVoteinitiateList();
			    } else {
			      this.$Message.warning(res.data.message);
			    }
			  });		
	}

},

//验证表单信息
			validateForm2() {
				let _valid = false;
				this.$refs["formtaskhb"].validate(valid => {
					if (!valid) {
						this.$Message.error("请完善表单信息");
					} else {
						_valid = true;
					}
				});
				return _valid;
			},

//查看汇报详情
hblook(row){
this.formModelhb2.opened=true;
hblooks({uuid:row.missionjournaluuid}).then(res=>{
	this.formModelhb2.fields=res.data.data
})

},




huibaoshow(row){
				var shows=false;
				if(row.accomplish=="0"){//未完成的任务可以添加汇报
				shows=true;
				}
				return shows;
			},

						//添加汇报
			huibao(row){
				this.$refs["formtaskhb"].resetFields();//清空表单
				this.formModelhb.fields.completed="";
				this.formModelhb.fields.coordination="";
				this.$refs.inputer1.value = ''; // 清空file文件
				this.formModelhb.opened=true;
				this.formModelhb.fields.missionHeadline=row.missionHeadline;
				this.formModelhb.fields.establishName=this.$store.state.user.userName;
				this.formModelhb.fields.missionUUID=row.missionUuid;
			},





			rowClassName (row, index) {
                if (row.auditStatus === "1"||row.auditStatus === "2") {
                    return 'demo-table-info-row';
                } else if (row.auditStatus === "3") {
                    return 'demo-table-error-row';
                }
                return '';
            },
			//获取调度数据
			loadVoteinitiateList() {
				this.stores.vote.query.userguid = this.$store.state.user.userGuid; //当前登录账号guid
				//获取当前账号负责的任务信息
				getdatalist(this.stores.vote.query).then(res => {
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
			//搜索
			handleSearch() {
				this.loadVoteinitiateList();
			},
			//点击添加打开窗口
			handleShowCreateWindow() {
				this.formModel.mode = "create"; //添加
				this.formModel.opened = true; //打开窗口
				this.$refs["formtask"].resetFields(); //清空填写的表单信息
				this.formModel.fields.establishName = this.$store.state.user.userName; //创建人
			},
			//点击编辑打开窗口
			handleEdit(row) {
				this.formModel.mode = "edit"; //修改
				this.formModel.opened = true;
				this.rowid = row.id; //保存选择行的id
				loaddata({
					id: row.id
				}).then(res => {
					this.formModel.fields = res.data.data
				});
			},
			//重点工作下拉框
			loadpersonaldiary() {
				getpersonaldiary().then(res => {
					this.prioritydata = res.data.data
					/* 			console.log("重点工作数据")
								console.log(this.prioritydata) */
				});
			},
			//负责人和审批人下拉框
			loadfuzeren() {
				getfuzeren().then(res => {
					this.approver = this.fuzerendata = res.data.data
				});
			},
			//穿梭框
			loaselect() {
				getcanyuren().then(res => {
					this.formModelcanyu.fields.participantleft = res.data.data
				});
			},
			//打开选择参与人窗口
			selectcanyu(rowid) {
				this.formModelcanyu.opened = true
				this.formModel.fields.selectcanyurenname = this.formModel.fields.participant = "";
				//修改
				if (rowid != "") {
					binddataok({
						id: rowid
					}).then(res => {
						this.formModelcanyu.fields.participantright = res.data.data;
						if (res.data.data.length > 0) {
							for (let i = 0; i < res.data.data.length; i++) {
								this.formModelcanyu.fields.participantright = this.formModelcanyu.fields.participantright.concat(res.data.data[
									i].key);
								this.formModel.fields.selectcanyurenname += res.data.data[i].label + ",";
								this.formModel.fields.participant += res.data.data[i].key + ",";
							}
						}
					});
				}

			},
			//选择参与人
			handleSchoolChange(newTargetKeys, direction, moveKeys) {
				this.formModelcanyu.fields.participantright = newTargetKeys;
				this.formModel.fields.selectcanyurenname = this.formModel.fields.participant = ""; //每次选择则参与人都重新赋值
				for (let i = 0; i < this.formModelcanyu.fields.participantright.length; i++) {
					for (let j = 0; j < this.formModelcanyu.fields.participantleft.length; j++) {
						if (this.formModelcanyu.fields.participantright[i] === this.formModelcanyu.fields.participantleft[j].key) {
							this.formModel.fields.selectcanyurenname += this.formModelcanyu.fields.participantleft[j].label + ",";
							this.formModel.fields.participant += this.formModelcanyu.fields.participantleft[j].key + ",";
						}
					}
				}
				/* 		  console.log("选择的人员")
						  console.log(this.selectcanyurenname) */
			},
			//点击保存
			handleSubmit() {
				if (this.formModel.mode === "create") { //保存添加的内容
					this.formModel.fields.establisthtruename = this.$store.state.user.userGuid; //当前登录账号guid(数据库保存创建人)
					create(this.formModel.fields).then(res => {
						if (res.data.code === 200) {
							this.$Message.success(res.data.message);
							this.formModel.opened = false;
							this.loadVoteinitiateList();
						} else {
							this.$Message.warning(res.data.message);
						}
					});
				}
				if (this.formModel.mode === "edit") { //保存修改的内容

					baocunedit(this.formModel.fields).then(res => {
						if (res.data.code === 200) {
							this.$Message.success(res.data.message);
							this.formModel.opened = false;
							this.loadVoteinitiateList();
						} else {
							this.$Message.warning(res.data.message);
						}
					});
				}
			},
			//删除多条数据
			handleBatchCommand(command) {
				if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
					this.$Message.warning("请选择至少一条数据");
					return;
				}
				this.$Modal.confirm({
					title: "操作提示",
					content: "<p确定要将所选任务放入回收站吗?</p>",
					loading: true,
					onOk: () => {
						/* 			  console.log("删除选择的")
							  		console.log(this.selectedRowsId.join(",")) */
						deletelist({
							ids: this.selectedRowsId.join(",")
						}).then(res => {
							if (res.data.code === 200) {
								this.$Message.success(res.data.message);
								this.loadVoteinitiateList();
								this.$Modal.remove(); //关闭提示框
							} else {
								this.$Message.warning(res.data.message);
							}
						});

					}
				});
			},
			//删除单条数据
			handleDelete(row) {
				/* console.log(row.id) */
				deletelist({
					ids: row.id
				}).then(res => {
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
				this.lookshow=true;
				this.huibaoshows=false;
				this.model2with=600;
				//this.$refs.inputer1.value = '';//清空file文件
				this.yijianshow = this.beizhushow = false;
				this.formModel2.opened = true;
				loaddata({
					id: row.id
				}).then(res => {
					this.formModel2.fields = res.data.data
					if (row.auditStatus != "0") //已提交完成则显示备注
					{
						this.beizhushow = true;
					}
					if (res.data.data.auditOpinion!="" &&res.data.data.auditOpinion!=null) //显示审核信息
					{
						this.yijianshow = true;
					}
				});
				this.rowuuid = row.missionUuid;
			},


			qitashow(e) {
				if (e == this.$store.state.user.userGuid) {
					return false;
				} else {
					return true;
				}
			},
			//判断是图片还是文件
			picshow(e) {
				let a = e.split('.');
				if (a[1] == "JPG" || a[1] == "GIF" || a[1] == "BMP" || a[1] == "JPEG" || a[1] == "PNG" || a[1] == "jfg" || a[1] == "gif" || a[1] == "bmp" || a[1] == "jpeg" || a[1] == "png") {
					return false;
				} else {
					return true;
				}
			},
			dangqianshow(e) {

				if (e == this.$store.state.user.userGuid) {
					return true;
				} else {
					return false;
				}
			},
			//发送操作日志
			fasong() {
				this.formmodelrizhi.missionUuid = this.formModel2.fields.missionUUID;
				this.formmodelrizhi.establishName = this.$store.state.user.userGuid;
				appcreaterizhi(this.formmodelrizhi).then(res => {
					if (res.data.code === 200) {
						this.formmodelrizhi.content = "";
						this.$Message.success(res.data.message);
						$Message.success("操作成功");

						console.log(this.formmodelrizhi.content)
					} else {
						this.$Message.warning(res.data.message);
					}
				})

			},
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
					alert("文件格式不正确!");
					return false;
				}
				// 通过DOM取文件数据
				this.fil = inputDOM.files;
				for (let i = 0; i < this.fil.length; i++) {
					let size = Math.floor(this.fil[i].size / 1024);
					if (size > 2 * 1024 * 1024) {
						alert("请选择2M以内的文件！");
						return false;
					}
					formData.append("multipartFiles", this.fil[i], this.fil[i].name);
					RegistPicture(formData).then(res => {
						if (res.data.code == "200") {
							this.$Message.success(res.data.msg);
							this.img1 = this.url + res.data.path;
							this.imgshow1 = true;
							this.imgcopy1 = res.data.path;
							this.imgname = res.data.path;
							// this.formModel.fields.accessory = res.data.path;
							this.formModelhb.fields.accessory = res.data.path;
						} else {
							this.$Message.warning(res.data.msg);
						}
					});
				}
			},
			//下载附件
			handleimportmodel(e) {
				window.location.href = this.url + "UploadFiles/PersonalDiary/" + e
			},
			//显示图片
			imageList(e) {
				return this.url + "UploadFiles/PersonalDiary/" + e
			},

			getauditStatus(auditStatus) {
				var auditStatusText = "未知";
				switch (auditStatus) {
					case "0":
						auditStatusText = "进行中";
						break;
					case "1":
						auditStatusText = "进行中";
						break;
					case "2":
						auditStatusText = "已完成";
						break;
						case "3":
						auditStatusText = "逾期";
						break;
				}
				return auditStatusText;
			}
		},
		mounted() {
			this.loadVoteinitiateList(); //加载所有数据
			this.loadpersonaldiary(); //重点工作下拉框
			this.loadfuzeren(); //负责人下拉框
			this.loaselect(); //穿梭框
			this.url = config.baseUrl.dev;
		}
	}
</script>

<style>
</style>
